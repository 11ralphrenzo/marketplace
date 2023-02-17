using marketplace.Utilities;

namespace marketplace.Resources;

// Request
public sealed record RegisterRequest(
    string Email, 
    string Password,
    Roles role);

// Response
public sealed record RegisterResponse(
    int Id, 
    string Email,
     Roles role,
    string Token);
