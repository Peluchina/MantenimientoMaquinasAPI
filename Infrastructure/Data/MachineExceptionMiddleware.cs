using FluentValidation;
using Microsoft.Data.SqlClient;
using MantenimientoMaquinasAPI.Infrastructure.Repository;

namespace MantenimientoMaquinasAPI.Infrastructure.Data;

public sealed class MachineExceptionMiddleware(RequestDelegate next, ILogger<MachineExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch (ValidationException exception)
        {
            var errors = exception.Errors.GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key, group => group.Select(error => error.ErrorMessage).ToArray());
            await Results.ValidationProblem(errors, statusCode: StatusCodes.Status400BadRequest).ExecuteAsync(context);
        }
        catch (MachineHasRelatedRecordsException exception)
        {
            await Results.Problem(statusCode: StatusCodes.Status409Conflict, title: "Conflicto",
                detail: exception.Message).ExecuteAsync(context);
        }
        catch (SqlException exception) when (exception.Number is 50001 or 50002 or 50003 or 50004)
        {
            await Results.Problem(statusCode: StatusCodes.Status400BadRequest, title: "Solicitud inválida",
                detail: "Los datos enviados no cumplen las reglas de la máquina.").ExecuteAsync(context);
        }
        catch (SqlException exception) when (exception.Number is 50005 or 50006)
        {
            await Results.Problem(statusCode: StatusCodes.Status404NotFound, title: "Máquina no encontrada",
                detail: "No existe una máquina con el identificador indicado.").ExecuteAsync(context);
        }
        catch (SqlException exception) when (exception.Number is 50101 or 50102 or 50103)
        {
            await Results.Problem(statusCode: StatusCodes.Status400BadRequest, title: "Solicitud inválida",
                detail: "Los datos enviados no cumplen las reglas del usuario o el rol indicado no existe.").ExecuteAsync(context);
        }
        catch (SqlException exception) when (exception.Number is 50104 or 50105)
        {
            await Results.Problem(statusCode: StatusCodes.Status404NotFound, title: "Usuario no encontrado",
                detail: "No existe un usuario con el identificador indicado.").ExecuteAsync(context);
        }
        catch (SqlException exception) when (exception.Number is 2601 or 2627)
        {
            await Results.Problem(statusCode: StatusCodes.Status409Conflict, title: "Conflicto",
                detail: "El nombre de usuario o correo electrónico ya está registrado.").ExecuteAsync(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error no controlado al procesar {Path}", context.Request.Path);
            await Results.Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Error interno",
                detail: "Ocurrió un error al procesar la solicitud.").ExecuteAsync(context);
        }
    }
}
