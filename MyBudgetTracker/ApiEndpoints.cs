namespace MyBudgetTracker;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Auth
    {
        private const string Base = $"{ApiBase}/auth";

        public const string Registration = $"{Base}/registration";
        public const string Confirmation = $"{Base}/confrimation";
        public const string Login = $"{Base}/login";
    }

    public static class Category
    {
        private const string Base = $"{ApiBase}/categories";

        public const string Create = $"{Base}";
        public const string GetAll = $"{Base}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
    }

    public static class Transaction
    {
        private const string Base = $"{ApiBase}/transactions";

        public const string Create = $"{Base}";
        public const string GetAll = $"{Base}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
    }

    public static class Budget
    {
        private const string Base = $"{ApiBase}/budgets";

        public const string Create = $"{Base}";
        public const string GetAll = $"{Base}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}