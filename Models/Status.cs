using System;

namespace DotNetAPI.Models
{
    public class Status
    {
        public Guid Id { get; set; }
        public string Label { get; set; }

        public const string NEW = "New";
        public const string IN_PROCESS = "In Process";
        public const string SUCCESSFULLY_FINISHED = "Successfully finished";
        public const string FAILED = "Failed";
    }
}