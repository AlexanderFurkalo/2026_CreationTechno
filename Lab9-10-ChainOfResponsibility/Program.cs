using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10_ChainOfResponsibility
{
    enum RequestLevel
    {
        Basic,
        Intermediate,
        Advanced
    }

    class SupportRequest
    {
        public string Description { get; set; }
        public RequestLevel Level { get; set; }

        public SupportRequest(string description, RequestLevel level)
        {
            Description = description;
            Level = level;
        }
    }

    abstract class SupportHandler
    {
        protected SupportHandler successor;

        public void SetSuccessor(SupportHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(SupportRequest request);
    }

    class BasicSupport : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Level == RequestLevel.Basic)
            {
                Console.WriteLine($"BasicSupport handled: {request.Description}");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }

    class IntermediateSupport : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Level == RequestLevel.Intermediate)
            {
                Console.WriteLine($"IntermediateSupport handled: {request.Description}");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }

    class SeniorSupport : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Level == RequestLevel.Advanced)
            {
                Console.WriteLine($"SeniorSupport handled: {request.Description}");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
            else
            {
                Console.WriteLine($"Request not handled: {request.Description}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SupportHandler basic = new BasicSupport();
            SupportHandler intermediate = new IntermediateSupport();
            SupportHandler senior = new SeniorSupport();

            basic.SetSuccessor(intermediate);
            intermediate.SetSuccessor(senior);

            var requests = new List<SupportRequest>
        {
            new SupportRequest("Reset password", RequestLevel.Basic),
            new SupportRequest("Server crash", RequestLevel.Advanced),
            new SupportRequest("Software installation issue", RequestLevel.Intermediate),
            new SupportRequest("Unknown critical bug", RequestLevel.Advanced)
        };

            foreach (var request in requests)
            {
                basic.HandleRequest(request);
            }

            Console.ReadKey();
        }
    }
}
