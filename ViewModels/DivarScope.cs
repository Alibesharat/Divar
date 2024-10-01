
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace divar.ViewModels
{
    public enum DivarScope
    {
        /// <summary>
        /// شماره  موبایل کاربر
        /// </summary>
        
        [Display(Name = "USER_PHONE")]

        USER_PHONE, 
        /// <summary>
        /// اضافه کردن افزونه به پست
        /// </summary>
       [Display(Name = "POST_ADDON_CREATE")]
        POST_ADDON_CREATE,
        /// <summary>
        /// ارسال پیام در چت
        /// </summary>
        [Display(Name = "CHAT_MESSAGE_SEND")]
        CHAT_MESSAGE_SEND,
        /// <summary>
        /// رفرش توکن
        /// </summary>
        [Display(Name = "OFFLINE_ACCESS")]
        OFFLINE_ACCESS // for refresh tokens

    }
}