namespace marketplace.Resources;

public sealed record UserResource(int Id, string Email, string? token = null);