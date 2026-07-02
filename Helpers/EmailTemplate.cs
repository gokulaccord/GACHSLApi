namespace GACHSLApi.Helpers
{
    public static class EmailTemplate
    {
        public static string GetOtpTemplate(string userName, string otp)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f5f5f5;
            padding: 20px;
        }}

        .container {{
            max-width: 600px;
            margin: auto;
            background: white;
            border-radius: 8px;
            padding: 30px;
            border: 1px solid #dddddd;
        }}

        .header {{
            background: #0d6efd;
            color: white;
            padding: 15px;
            text-align: center;
            font-size: 22px;
            border-radius: 6px;
        }}

        .otp {{
            font-size: 32px;
            font-weight: bold;
            color: #0d6efd;
            text-align: center;
            letter-spacing: 6px;
            margin: 25px 0;
        }}

        .footer {{
            margin-top: 30px;
            font-size: 12px;
            color: gray;
            text-align: center;
        }}
    </style>
</head>

<body>

<div class='container'>

<div class='header'>
GACHSL Password Reset
</div>

<p>Hello <strong>{userName}</strong>,</p>

<p>We received a request to reset your password.</p>

<p>Please use the following One-Time Password (OTP):</p>

<div class='otp'>
{otp}
</div>

<p>
This OTP is valid for <strong>10 minutes</strong>.
</p>

<p>
If you didn't request this password reset, you can safely ignore this email.
</p>

<div class='footer'>
© 2026 GACHSL. All Rights Reserved.
</div>

</div>

</body>
</html>";
        }
    }
}