namespace Assertly.Core;
public record AndWhichConstraint<TParent, TSubject>(TParent And, TSubject Subject) :
    AndConstraint<TParent>(And) where TParent : notnull;
