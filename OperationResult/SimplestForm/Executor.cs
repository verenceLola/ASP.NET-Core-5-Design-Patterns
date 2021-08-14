using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OperationResult.SimplestForm
{
    public class Executor
    {
        public OperationResult Operation()
        {
            var randomNumber = new Random().Next(100);
            var succes = randomNumber % 2 == 0;

            return new OperationResult(succes);
        }
    }

    public class OperationResult
    {
        public OperationResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public bool Succeeded { get; }
    }
}


namespace OperationResult.SingleError
{
    public class OperationResult
    {
        public OperationResult() { }
        public OperationResult(string errorMessage)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }
        public bool Succeeded => string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; }
    }

    public class Executor
    {
        public OperationResult Operation()
        {
            var randomNumber = new Random().Next(100);
            var success = randomNumber % 2 == 0;

            return success ? new OperationResult() : new OperationResult($"Something went wrong with the number '{randomNumber}'.");
        }
    }
}

namespace OperationResult.SingleErrorWithValue
{
    public class OperationResult
    {
        public int? Value { get; init; }

        public OperationResult() { }
        public OperationResult(string errorMessage)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }
        public bool Succeeded => string.IsNullOrWhiteSpace(ErrorMessage);

        public string ErrorMessage { get; }
    }

    public class Executor
    {
        public OperationResult Operation()
        {
            var randomNumber = new Random().Next(100);
            var success = randomNumber % 2 == 0;

            return success ? new OperationResult
            {
                Value = randomNumber
            } : new OperationResult($"Something went wrong with the number '{randomNumber}'.")
            {
                Value = randomNumber
            };
        }
    }
}

namespace OperationResult.MultipleErrorsWithValue
{
    public class OperationResult
    {
        private readonly List<string> _errors;
        public OperationResult(params string[] errors)
        {
            _errors = new List<string>(errors ?? Enumerable.Empty<string>());
        }

        public bool Succeeded => !HasErrors();
        public int? Value { get; set; }
        public IEnumerable<string> Errors => new ReadOnlyCollection<string>(_errors);
        public bool HasErrors() => Errors?.Count() > 0;
        public void AddError(string message)
        {
            _errors.Add(message);
        }
    }

    public class Executor
    {
        public OperationResult Operation()
        {
            var randomNumber = new Random().Next(100);
            var success = randomNumber % 2 == 0;

            return success ? new OperationResult
            {
                Value = randomNumber
            } : new OperationResult($"Something went wrong with the number '{randomNumber}'.")
            {
                Value = randomNumber
            };
        }
    }
}

namespace OperationResult.WithSeverity
{
    public class OperationResultMessage
    {
        public OperationResultMessage(string message, OperationSeverity severity)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
        }
        public string Message { get; }
        public OperationSeverity Severity { get; }
    }

    public enum OperationSeverity
    {
        Information = 0,
        Warning = 1,
        Error = 2
    }

    public enum OperationStatus
    {
        Success = 0,
        Failure = 1,
        PartialFailure = 2,
    }

    public class OperationResult
    {
        private readonly List<OperationResultMessage> _messages;
        public OperationResult(params OperationResultMessage[] errors)
        {
            _messages = new List<OperationResultMessage>(errors ?? Enumerable.Empty<OperationResultMessage>());
        }
        public bool Succeeded => !HasErrors();
        public OperationStatus? Status => StatusInfo();
        private OperationStatus? StatusInfo()
        {
            {
                var failed = _messages.Any(x => x.Severity == OperationSeverity.Error);
                var information = _messages.Any(x => x.Severity == OperationSeverity.Information);
                var warning = _messages.Any(x => x.Severity == OperationSeverity.Warning);

                Nullable<OperationStatus> operationStatus = null;

                if (failed)
                {
                    operationStatus = OperationStatus.Failure;
                }
                else if (warning)
                {
                    operationStatus = OperationStatus.PartialFailure;
                }
                else if (Succeeded)
                {
                    operationStatus = OperationStatus.Success;
                }

                return operationStatus;
            }
        }
        public int? Value { get; init; }
        public IEnumerable<OperationResultMessage> Messages => new ReadOnlyCollection<OperationResultMessage>(_messages);
        public bool HasErrors() => FindErrors().Count() > 0;
        public void AddMessage(OperationResultMessage message)
        {
            _messages.Add(message);
        }
        private IEnumerable<OperationResultMessage> FindErrors() =>
            _messages.Where(x => x.Severity == OperationSeverity.Error);
    }

    public class Executor
    {
        public OperationResult Operation()
        {
            var randomNumber = new Random().Next(100);
            var success = randomNumber % 2 == 0;

            var information = new OperationResultMessage("This should be very important!", OperationSeverity.Information);

            if (success)
            {
                var warning = new OperationResultMessage("Something went wrong, we'll retry auto until it works!", OperationSeverity.Warning);

                return new OperationResult(information, warning) { Value = randomNumber };
            }
            else
            {
                var error = new OperationResultMessage($"Something went wrong with the number '{randomNumber}'.", OperationSeverity.Error);

                return new OperationResult(error) { Value = randomNumber };
            }
        }
    }
}

namespace OperationResult.StaticFactoryMethod
{
    public class OperationResultMessage
    {
        public OperationResultMessage(string message, OperationResultSeverity severity)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
        }
        public string Message { get; }
        public OperationResultSeverity Severity { get; }
    }
    public enum OperationResultSeverity
    {
        Information = 0,
        Warning = 1,
        Error = 2,
    }
    public abstract class OperationResult
    {
        private OperationResult() { }
        public abstract bool Succeeded { get; }
        public virtual int? Value { get; init; }
        public abstract IEnumerable<OperationResultMessage> Messages { get; }
        public static OperationResult Success(int? value = null) => new SuccessfulOperationResult { Value = value };
        public static OperationResult Failure(params OperationResultMessage[] errors) => new FailedOperationResult(errors);
        public sealed class SuccessfulOperationResult : OperationResult
        {
            public override bool Succeeded => true;
            public override IEnumerable<OperationResultMessage> Messages => Enumerable.Empty<OperationResultMessage>();
        }
        public sealed class FailedOperationResult : OperationResult
        {
            private readonly List<OperationResultMessage> _messages;
            public FailedOperationResult(params OperationResultMessage[] errors)
            {
                _messages = new List<OperationResultMessage>(errors ?? Enumerable.Empty<OperationResultMessage>());
            }
            public override bool Succeeded => false;
            public override IEnumerable<OperationResultMessage> Messages => new ReadOnlyCollection<OperationResultMessage>(_messages);
        }
    }

    public class Executor
    {
        public OperationResult Operation()
        {
            var randomNumber = new Random().Next(100);
            var success = randomNumber % 2 == 0;

            if (success)
            {
                return OperationResult.Success(randomNumber);
            }
            else
            {
                var error = new OperationResultMessage($"Something went wrong with the number '{randomNumber}'.", OperationResultSeverity.Error);

                return OperationResult.Failure(error);
            }
        }
    }
}
