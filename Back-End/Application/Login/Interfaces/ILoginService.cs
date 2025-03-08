using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Login.DTOs;

namespace Application.Login.Interfaces;

public interface ILoginService
{
    Task<Result> Login(LoginDTO login);
    Task<Result<ResultLoginDTO>> CodigoEmailValido(CodigoLoginDTO codigoLoginDTO);
}
