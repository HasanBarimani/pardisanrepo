using System;

namespace Pardisan.ViewModels.Message
{
    public class MessageVM
    {
        public int? Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }

        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
