using System;

namespace BLL.Exceptions
{
    public class ParticipantException : Exception
    {
        public ParticipantException(string message) : base(message) { }
    }

    public class InvalidParticipantDataException : ParticipantException
    {
        public InvalidParticipantDataException(string message) : base(message) { }
    }

    public class FileOperationException : ParticipantException
    {
        public FileOperationException(string message) : base(message) { }
    }

    public class ChessGameException : ParticipantException
    {
        public ChessGameException(string message) : base(message) { }
    }

}