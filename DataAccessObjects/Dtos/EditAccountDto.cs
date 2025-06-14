﻿
namespace DataAccessObjects.Dtos
{
    public class EditAccountDto
    {
        public short AccountId { get; set; }

        public string? AccountName { get; set; }

        public string? AccountEmail { get; set; }

        public int? AccountRole { get; set; }

        public string? OldPassword { get; set; }
        public string? AccountPassword { get; set; }
        public string? ConfirmPass { get; set; }
    }
}
