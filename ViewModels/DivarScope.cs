
namespace divar.ViewModels
{
    public enum DivarScope
    {
        /// <summary>
        /// شماره  موبایل کاربر
        /// </summary>
        USER_PHONE, 
        /// <summary>
        /// اضافه کردن افزونه به پست
        /// </summary>
        POST_ADDON_CREATE,
        /// <summary>
        /// ارسال پیام در چت
        /// </summary>
        CHAT_MESSAGE_SEND,
        /// <summary>
        /// رفرش توکن
        /// </summary>
        OFFLINE_ACCESS // for refresh tokens

    }
}