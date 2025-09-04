using fly_server.Enums;

namespace fly_server.Constants;

public static class ErrorMessages
{
    public static readonly Dictionary<ApiErrorKey, string> ErrorMaps = new()
    {
        { ApiErrorKey.SomethingWentWrong, "Something went wrong!" },
        { ApiErrorKey.CreateUserFailed, "Create user failed!" },
        { ApiErrorKey.DeleteUserFailed, "Delete user failed!" },
        { ApiErrorKey.UserNotFound, "User(s) not found!" },
        { ApiErrorKey.InvalidCaptcha, "Invalid captcha!" },
        { ApiErrorKey.InternalServerError, "Internal server error!" },
        { ApiErrorKey.InvalidCredentials, "Invalid username/password!" },
        { ApiErrorKey.NoDataFound, "No data found!" },
        { ApiErrorKey.DestinationAddFailed, "Failed to  add destination!" },
        { ApiErrorKey.DestinationDeleteFailed, "Failed to  delete destination!" },
        { ApiErrorKey.DestinationUpdateFailed, "Failed to  update destination!" },
        { ApiErrorKey.FailedToInsertAirline, "Failed to insert airline!" },
        { ApiErrorKey.FailedToUpdateAirline, "Failed to  update airline!" },
    };
}