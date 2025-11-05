using PoolApp.Domain.EntitiesUsers;
using OtpNet;
using System.Web;
using QRCoder;
using System;
using System.IO;

namespace PoolApp.Services
{
    public class AuthenticatorService
    {


        public string GenerateSetupCode(string issuer, string userPhone, out string base32Secret)
        {
            // Generate a 20-byte secret key
            byte[] secretKey = KeyGeneration.GenerateRandomKey(20);
            base32Secret = Base32Encoding.ToString(secretKey);

            // Encode for URL safety
            string encodedIssuer = HttpUtility.UrlEncode(issuer);
            string encodedAccount = HttpUtility.UrlEncode(userPhone);

            // Build the otpauth URL
            string otpauthUrl = $"otpauth://totp/{encodedIssuer}:{encodedAccount}?secret={base32Secret}&issuer={encodedIssuer}";

            return otpauthUrl; //use this to generate a QR code
        }

        public string GenerateQrCodeImageBase64(string qrData)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrBytes = qrCode.GetGraphic(20);
            return $"data:image/png;base64,{Convert.ToBase64String(qrBytes)}";
        }

        public bool VerifyCode(string base32Secret, string code)
        {
            var totp = new Totp(Base32Encoding.ToBytes(base32Secret));
            // Allow 2 time steps (±60 seconds tolerance)
            return totp.VerifyTotp(code, out _, new VerificationWindow(2, 2));
        }

       
    }
}
