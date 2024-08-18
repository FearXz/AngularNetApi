namespace AngularNetApi.Core.Exceptions
{
    public class ServerErrorException : Exception
    {
        public ServerErrorException() { }

        public ServerErrorException(string message)
            : base(message) { }

        public ServerErrorException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message)
            : base(message) { }

        public NotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException() { }

        public BadRequestException(string message)
            : base(message) { }

        public BadRequestException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }

        public UnauthorizedException(string message)
            : base(message) { }

        public UnauthorizedException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class LockedOutException : Exception
    {
        public LockedOutException() { }

        public LockedOutException(string message)
            : base(message) { }

        public LockedOutException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException() { }

        public ForbiddenException(string message)
            : base(message) { }

        public ForbiddenException(string message, Exception inner)
            : base(message, inner) { }
    }
}
