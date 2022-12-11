namespace PureMVC.Interfaces
{
    using System;

    public interface IController
    {
        //执行命令
        void ExecuteCommand(INotification notification);
        //是否存在命令
        bool HasCommand(string notificationName);
        //注册命令
        void RegisterCommand(string notificationName, Type commandType);
        //移除命令
        void RemoveCommand(string notificationName);
    }
}