using Microsoft.AspNetCore.Mvc;
using RecipeParser.Api.Models;
using RecipeParser.Api.Services;
using RecipeParser.Api.Parser;

namespace RecipeParser.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly TheMealDBService _mealService;

        public RecipeController(TheMealDBService mealService)
        {
            _mealService = mealService;
        }

        /// <summary>
        /// Get dropdown options for the UI (first 5 of each type)
        /// </summary>
        [HttpGet("dropdown-options")]
        public async Task<ActionResult<DropdownOptionsResponse>> GetDropdownOptions()
        {
            try
            {
                var options = await _mealService.GetDropdownOptionsAsync();
                return Ok(options);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to fetch dropdown options", details = ex.Message });
            }
        }

        /// <summary>
        /// Search recipes using Boolean query
        /// </summary>
        [HttpPost("search")]
        public async Task<ActionResult<RecipeSearchResponse>> SearchRecipes([FromBody] RecipeSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Query))
            {
                return BadRequest(new { error = "Query is required" });
            }

            try
            {
                // Parse the Boolean query
                var parseResult = SimpleRecipeParser.ParseQuery(request.Query);
                if (!parseResult.Success)
                {
                    return BadRequest(new { error = "Invalid query syntax", details = parseResult.ErrorMessage });
                }

                // For now, implement basic search - in a real implementation, you'd evaluate the AST
                var recipes = await SearchRecipesBySimpleTerms(request.Query);

                var response = new RecipeSearchResponse
                {
                    Query = request.Query,
                    Results = recipes,
                    ParseTree = GenerateSimpleParseTree(parseResult.Value),
                    ExecutionTime = "2ms", // Placeholder
                    ResultCount = recipes.Count,
                    GeneratedSQL = GenerateSQL(request.Query)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Search failed", details = ex.Message });
            }
        }

        /// <summary>
        /// Validate a Boolean query syntax
        /// </summary>
        [HttpGet("validate")]
        public ActionResult<QueryValidationResponse> ValidateQuery([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { error = "Query parameter is required" });
            }

            try
            {
                var parseResult = SimpleRecipeParser.ParseQuery(query);

                var response = new QueryValidationResponse
                {
                    Query = query,
                    IsValid = parseResult.Success,
                    ErrorMessage = parseResult.ErrorMessage,
                    ParseTree = parseResult.Success ? GenerateSimpleParseTree(parseResult.Value) : null
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new QueryValidationResponse
                {
                    Query = query,
                    IsValid = false,
                    ErrorMessage = $"Validation error: {ex.Message}",
                    ParseTree = null
                });
            }
        }

        /// <summary>
        /// Get recipe by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById(string id)
        {
            try
            {
                var recipe = await _mealService.GetMealByIdAsync(id);
                if (recipe == null)
                {
                    return NotFound(new { error = $"Recipe with ID {id} not found" });
                }

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to fetch recipe", details = ex.Message });
            }
        }

        /// <summary>
        /// Simple search implementation (placeholder for full Boolean evaluation)
        /// </summary>
        private async Task<List<Recipe>> SearchRecipesBySimpleTerms(string query)
        {
            var allRecipes = new List<Recipe>();
            var searchTerms = ExtractSearchTerms(query);

            // Search by categories, areas, and ingredients
            foreach (var term in searchTerms)
            {
                // Try searching as category
                var categoryRecipes = await _mealService.GetMealsByCategoryAsync(term);
                allRecipes.AddRange(categoryRecipes);

                // Try searching as area
                var areaRecipes = await _mealService.GetMealsByAreaAsync(term);
                allRecipes.AddRange(areaRecipes);

                // Try searching as ingredient
                var ingredientRecipes = await _mealService.GetMealsByIngredientAsync(term);
                allRecipes.AddRange(ingredientRecipes);

                // Try searching by name
                var nameRecipes = await _mealService.SearchMealsByNameAsync(term);
                allRecipes.AddRange(nameRecipes);
            }

            // Remove duplicates and limit results
            return allRecipes
                .GroupBy(r => r.Id)
                .Select(g => g.First())
                .Take(20)
                .ToList();
        }

        /// <summary>
        /// Extract search terms from query (simplified)
        /// </summary>
        private List<string> ExtractSearchTerms(string query)
        {
            // Remove Boolean operators and parentheses for simple search
            var terms = query
                .Replace("AND", " ")
                .Replace("OR", " ")
                .Replace("NOT", " ")
                .Replace("(", " ")
                .Replace(")", " ")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(term => term.Length > 2)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return terms;
        }

        /// <summary>
        /// Generate simple parse tree visualization
        /// </summary>
        private object GenerateSimpleParseTree(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return null;

            var parts = query.Split(new[] { " AND ", " OR " }, StringSplitOptions.RemoveEmptyEntries);
            var operators = new List<string>();

            if (query.Contains(" AND ")) operators.Add("AND");
            if (query.Contains(" OR ")) operators.Add("OR");

            return new
            {
                Type = "BooleanQuery",
                Query = query,
                Terms = parts.Select(p => p.Trim()).ToArray(),
                Operators = operators.ToArray(),
                HasParentheses = query.Contains("(") || query.Contains(")"),
                HasNot = query.Contains("NOT")
            };
        }

        /// <summary>
        /// Generate SQL query from Boolean expression
        /// </summary>
        private string GenerateSQL(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return string.Empty;

            var sql = @"SELECT r.*, c.CategoryName, a.AreaName
FROM Recipes r
LEFT JOIN Categories c ON r.CategoryId = c.Id
LEFT JOIN Areas a ON r.AreaId = a.Id
LEFT JOIN RecipeIngredients ri ON r.Id = ri.RecipeId
LEFT JOIN Ingredients i ON ri.IngredientId = i.Id
WHERE ";

            // Transform Boolean logic to SQL
            var whereClause = ConvertBooleanToSQL(query);
            sql += whereClause;
            sql += "\nGROUP BY r.Id\nORDER BY r.Name;";

            return sql;
        }

        /// <summary>
        /// Convert Boolean query to SQL WHERE clause
        /// </summary>
        private string ConvertBooleanToSQL(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return "1=1";

            var sqlWhere = query;

            // Handle cuisines/areas
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere,
                @"\b(american|british|canadian|chinese|croatian|dutch|egyptian|filipino|french|greek|indian|irish|italian|jamaican|japanese|kenyan|malaysian|mexican|moroccan|polish|portuguese|russian|spanish|thai|tunisian|turkish|ukrainian|vietnamese)\b",
                "a.AreaName = '$1'",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Handle ingredients
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere,
                @"\b(chicken|salmon|beef|pork|avocado|lime|rice|onions|garlic|tomatoes|potatoes|carrots|mushrooms|peppers|cheese|butter)\b",
                "i.IngredientName LIKE '%$1%'",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Handle categories
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere,
                @"\b(breakfast|dessert|goat|lamb|miscellaneous|pasta|seafood|side|starter|vegan|vegetarian)\b",
                "c.CategoryName = '$1'",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Handle dietary restrictions and attributes
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere, @"\bvegetarian\b", "r.IsVegetarian = 1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere, @"\bvegan\b", "r.IsVegan = 1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere, @"\bgluten_free\b", "r.IsGlutenFree = 1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere, @"\bquick\b", "r.CookTimeMinutes <= 30", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere, @"\beasy\b", "r.DifficultyLevel = 'Easy'", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Handle NOT operator
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere, @"NOT\s+\(", "NOT (", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            sqlWhere = System.Text.RegularExpressions.Regex.Replace(sqlWhere, @"NOT\s+([^(]\S+)", "NOT $1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return sqlWhere;
        }
    }

    /// <summary>
    /// Request model for recipe search
    /// </summary>
    public class RecipeSearchRequest
    {
        public string Query { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response model for recipe search
    /// </summary>
    public class RecipeSearchResponse
    {
        public string Query { get; set; } = string.Empty;
        public List<Recipe> Results { get; set; } = new List<Recipe>();
        public object? ParseTree { get; set; }
        public string ExecutionTime { get; set; } = string.Empty;
        public int ResultCount { get; set; }
        public string GeneratedSQL { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response model for query validation
    /// </summary>
    public class QueryValidationResponse
    {
        public string Query { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
        public object? ParseTree { get; set; }
    }
}