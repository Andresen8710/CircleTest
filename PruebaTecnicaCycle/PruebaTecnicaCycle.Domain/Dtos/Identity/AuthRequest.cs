﻿namespace PruebaTecnicaCycle.Domain.Dtos.Identity
{
    public class AuthRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }=string.Empty;
    }
}