namespace BlogRest.Exceptions;

public class ArticleTitleExistsException : Exception
{
    public ArticleTitleExistsException()
    {

    }
    public ArticleTitleExistsException(string message)
        : base(message)
    {

    }
    public ArticleTitleExistsException(string message, Exception inner)
        : base(message, inner)
    {

    }
}