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
}