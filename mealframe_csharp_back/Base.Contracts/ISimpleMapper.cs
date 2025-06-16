namespace Base.Contracts;

public interface ISimpleMapper<TSource, TDestination>
{
    TDestination? Map(TSource? entity);
    TSource? Map(TDestination? entity);
}