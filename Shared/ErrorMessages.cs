namespace TaskManager.Shared
{
    // Static class with error messages
    public static class ErrorMessages
    {
        public static string dbErrorTaskId = "DB Error: Cant find task with this id";
        public static string dbErrorCommentId = "DB Error: Cant find comment with this id";
        public static string dbErrorResponsibleId = "DB Error: Cant find responsible with this id";
        public static string badRequestStatus = "Bad Request: Status not in range";
        public static string notFoundTasks = "Tasks not found with this data";

    }
}
