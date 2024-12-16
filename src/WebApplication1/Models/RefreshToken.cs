﻿using WebApplication1.Models;


namespace WebApplication1.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }   
        public Customer Account { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp {  get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get;set; }
        public string? ReplacedByToken { get; set; }
        public string?  ReasoneRevoked { get; set; }
        public bool IsExpire => DateTime.UtcNow > Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => Revoked == null && !IsExpire;


    }
}
