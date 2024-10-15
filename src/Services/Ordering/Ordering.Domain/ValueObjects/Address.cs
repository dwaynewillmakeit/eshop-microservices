namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipeCode { get; } = default!;

    protected Address() { }

    private Address(string firstName, string lastName, string? email,
        string addressLine, string country, string state, string zipeCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = email;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipeCode = zipeCode;
    }

    public static Address Of(
        string firstName,
        string lastName,
        string? email, string
        addressLine,
        string country,
        string state,
        string zipeCode
        )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        return new Address(
            firstName, lastName, email, addressLine, country, state, zipeCode
            );
    }
}

