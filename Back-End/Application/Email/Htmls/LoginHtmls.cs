﻿using System.Text;

namespace Application.Email.Htmls;

public static class LoginHtmls
{
    private static readonly StringBuilder builderHtmlLogin = new StringBuilder(""" 
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bem-vindo!</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            padding: 10px;
        }
        .container {
            max-width: 600px;
            margin: 20px auto;
            background: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        h1 {
            color: #333;
        }
        p {
            color: #666;
            font-size: 16px;
        }
        .codigo {
            display: inline-block;
            padding: 11px 20px;
            margin-top: 20px;
            color: #fff;
            background-color: #007bff;
            text-decoration: none;
            border-radius: 5px;
            font-size: 18px;
        }

        .footer {
            margin-top: 20px;
            font-size: 12px;
            color: #999;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Bem-vindo ao App Finanças!</h1>
        <p>Olá, estamos felizes em tê-lo conosco! você faz parte da nossa comunidade.</p>
        <p>Seu codigo para login:</p>
        <p class="codigo">@codigo</p>
        <p class="footer">Se você não solicitou o login, ignore este e-mail.</p>
    </div>
</body>
</html>
""");

    public static string ObterHtmlLogin(string codigo)
    {
        return builderHtmlLogin.ToString().Replace("@codigo", codigo);
    }
}
