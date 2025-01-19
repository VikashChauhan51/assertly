namespace Assertly;

/// <summary>
/// Initializes a new instance of the <see cref="AndConstraint{T}"/> class.
/// </summary>
public record AndConstraint<TParent>(TParent And) where TParent : notnull;

