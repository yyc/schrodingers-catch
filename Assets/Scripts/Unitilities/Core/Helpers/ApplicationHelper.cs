// <summary>
/// ApplicationHelper v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Shortcuts for common queries to Unity's Application.
/// </summary>

using UnityEngine;

namespace Unitilities
{

    public static class ApplicationHelper
    {
        
        #region Platforms

        public static bool PlatformIsDesktop
        {
            get
            {
                return ApplicationHelper.PlatformIsEditor ||
                    ApplicationHelper.PlatformIsDesktopStandalone ||
                    ApplicationHelper.PlatformIsWeb;
            }
        }

        public static bool PlatformIsDesktopStandalone
        {
            get
            {
				return Application.platform == RuntimePlatform.WindowsPlayer ||
				Application.platform == RuntimePlatform.LinuxPlayer ||
				Application.platform == RuntimePlatform.OSXPlayer ||

                    #if UNITY_5
                        Application.platform == RuntimePlatform.WSAPlayerX86 ||
                        Application.platform == RuntimePlatform.WSAPlayerX64;
                    #endif
				false;
            }
        }

        public static bool PlatformIsEditor
        {
            get
            {
                return Application.isEditor;

                /*return Application.platform == RuntimePlatform.WindowsEditor ||
                   Application.platform == RuntimePlatform.OSXEditor;*/
            }
        }

        public static bool PlatformIsWeb
        {
            get
            {
				return 
                        #if UNITY_5
					Application.platform == RuntimePlatform.WebGLPlayer||
						#endif
					false;
            }
        }

        public static bool PlatformIsMobile
        {
            get
            {
                return Application.platform == RuntimePlatform.Android ||
                       Application.platform == RuntimePlatform.IPhonePlayer ||
                       
                       #if UNITY_5
                        Application.platform == RuntimePlatform.WSAPlayerARM;
						#else
					false;
                       #endif
            }
        }



        #endregion

    }

}