﻿namespace WebApi;

public class ApiResultError
{
    public List<string> Errors { get; set; } = new List<string>();

    protected ApiResultError(string message)
    {
        this.Errors.Add(message);
    }

    protected ApiResultError(List<string> erros)
    {
        this.Errors.AddRange(erros);
    }
    public void AddErro(string message)
    {
        Errors.Add(message);
    }

    public static ApiResultError Create(Error error)
        => new ApiResultError(error.Message);

    public static ApiResultError Create(string message)
         => new ApiResultError(message);

    public static ApiResultError Create(List<string> erros)
     => new ApiResultError(erros);
}
