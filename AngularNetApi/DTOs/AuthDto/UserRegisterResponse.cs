﻿namespace AngularNetApi.DTOs.AuthDto
{
    public class UserRegisterResponse
    {
        public string NewUserId { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}