using CashOverflow.Models.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CashOverflow.Models
{
    public class Note
    {
        public Note()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        [EnumDataType(typeof(NoteStatus))]
        public NoteStatus Status { get; set; }

        public DateTime DateCreated { get; set; }

        public string BackgroundColor { get; set; }

        public string TextColor { get; set; }

        public bool IsSticky { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
