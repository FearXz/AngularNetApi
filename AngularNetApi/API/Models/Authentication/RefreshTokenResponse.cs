﻿namespace AngularNetApi.API.Models.Authentication
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}