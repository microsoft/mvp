using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Mvp.Helpers
{
	public class CommonConstants
	{
		private CommonConstants()
		{

		}

		#region ContributionType

		public const string AT = "Article";
		public const string Bsp = "Blog Site Posts";
		public const string BA = "Book (Author)";
		public const string Bca = "Book (Co-Author)";
		public const string CS = "Code Samples";
		public const string Cpt = "Code Project/Tools";
		public const string Cbp = "Conference (booth presenter)";
		public const string CO = "Conference (organizer)";
		public const string FM = "Forum Moderator";
		public const string FP = "Forum Participation (3rd Party forums)";
		public const string Fpmf = "Forum Participation (Microsoft Forums)";
		public const string MS = "Mentorship";
		public const string Osp = "Open Source Project(s)";
		public const string OT = "Other";
		public const string Pcf = "Product Connect Feedback";
		public const string Pgf = "Product Group Feedback";
		public const string Pgfg = "Product Group Feedback (General)";
		public const string Pgi = "Product Group Interaction (PGI)";
		public const string Ptdp = "Product Team DL Participation";
		public const string SO = "Site Owner";
		public const string SC = "Speaking (Conference)";
		public const string SL = "Speaking (Local)";
		public const string Sug = "Speaking (User group)";
		public const string Tsm = "Technical Social Media (Twitter, Facebook, LinkedIn...)";
		public const string Trfe = "Translation Review, Feedback and Editing";
		public const string Ugo = "User Group Owner";
		public const string VD = "Video";
		public const string WB = "Webcast";
		public const string WP = "WebSite Posts";

		#endregion

		#region ResourceKey list	
		public const string AnnualReachTipTextForArticle = "AnnualReachTipTextForArticle";

		public const string AnnualReachTipTextForPosts = "AnnualReachTipTextForPosts";

		public const string AnnualReachTipTextForBook = "AnnualReachTipTextForBook";

		public const string AnnualReachTipTextForSamples = "AnnualReachTipTextForSamples";

		public const string AnnualReachTipTextForProject = "AnnualReachTipTextForProject";

		public const string AnnualReachTipTextForConference = "AnnualReachTipTextForConference";

		public const string AnnualReachTipTextForForumModerator = "AnnualReachTipTextForForumModerator";

		public const string AnnualReachTipTextForForumParticipation = "AnnualReachTipTextForForumParticipation";

		public const string AnnualReachTipTextForMentorship = "AnnualReachTipTextForMentorship";

		public const string AnnualReachTipTextForOpenSource = "AnnualReachTipTextForOpenSource";

		public const string AnnualReachTipTextForOther = "AnnualReachTipTextForOther";

		public const string AnnualReachTipTextForFeedback = "AnnualReachTipTextForFeedback";

		public const string AnnualReachTipTextForSiteOwner = "AnnualReachTipTextForSiteOwner";

		public const string AnnualReachTipTextForSpeaking = "AnnualReachTipTextForSpeaking";

		public const string AnnualReachTipTextForSocialMedia = "AnnualReachTipTextForSocialMedia";

		public const string AnnualReachTipTextForTranslationReviewFeedbackEditing = "AnnualReachTipTextForTranslationReviewFeedbackEditing";

		public const string AnnualReachTipTextForUserGroupOwner = "AnnualReachTipTextForUserGroupOwner";

		public const string AnnualReachTipTextForVideo = "AnnualReachTipTextForVideo";

		public const string AnnualReachTipTextForWebsitePosts = "AnnualReachTipTextForWebsitePosts";

		public const string AnnualReachTipTextDefault = "AnnualReachTipTextDefault";

		public const string AnnualQuantityTipTextForArticle = "AnnualQuantityTipTextForArticle";

		public const string AnnualQuantityTipTextForPosts = "AnnualQuantityTipTextForPosts";

		public const string AnnualQuantityTipTextForBook = "AnnualQuantityTipTextForBook";

		public const string AnnualQuantityTipTextForSamples = "AnnualQuantityTipTextForSamples";

		public const string AnnualQuantityTipTextForProject = "AnnualQuantityTipTextForProject";

		public const string AnnualQuantityTipTextForConference = "AnnualQuantityTipTextForConference";

		public const string AnnualQuantityTipTextForForumModerator = "AnnualQuantityTipTextForForumModerator";

		public const string AnnualQuantityTipTextForForumParticipation = "AnnualQuantityTipTextForForumParticipation";

		public const string AnnualQuantityTipTextForMentorship = "AnnualQuantityTipTextForMentorship";

		public const string AnnualQuantityTipTextForOpenSource = "AnnualQuantityTipTextForOpenSource";

		public const string AnnualQuantityTipTextForOther = "AnnualQuantityTipTextForOther";

		public const string AnnualQuantityTipTextForFeedback = "AnnualQuantityTipTextForFeedback";

		public const string AnnualQuantityTipTextForSiteOwner = "AnnualQuantityTipTextForSiteOwner";

		public const string AnnualQuantityTipTextForSpeaking = "AnnualQuantityTipTextForSpeaking";

		public const string AnnualQuantityTipTextForSocialMedia = "AnnualQuantityTipTextForSocialMedia";

		public const string AnnualQuantityTipTextForTranslationReviewFeedbackEditing = "AnnualQuantityTipTextForTranslationReviewFeedbackEditing";

		public const string AnnualQuantityTipTextForUserGroupOwner = "AnnualQuantityTipTextForUserGroupOwner";

		public const string AnnualQuantityTipTextForVideo = "AnnualQuantityTipTextForVideo";

		public const string AnnualQuantityTipTextForWebsitePosts = "AnnualQuantityTipTextForWebsitePosts";

		public const string AnnualQuantityTipTextDefault = "AnnualQuantityTipTextDefault";

		public const string SecondAnnualQuantityTipTextForPosts = "SecondAnnualQuantityTipTextForPosts";

		public const string SecondAnnualQuantityTipTextForumParticipation = "SecondAnnualQuantityTipTextForumParticipation";

		public const string SecondAnnualQuantityTipTextForWebsitePosts = "SecondAnnualQuantityTipTextForWebsitePosts";

		public const string TabTitleForContributions= "TabTitleForContributions";
		public const string TabTitleForProfile = "TabTitleForProfile";

		public const string RefreshTokenTip = "RefreshTokenTip";

		public const string RefreshTokenExceptionTip = "RefreshTokenExceptionTip";

		public const string VersionFormat = "VersionFormat";

		public const string CopyrightFormat = "CopyrightFormat";

		public const string About = "About";
		public const string DefaultNetworkErrorString = "DefaultNetworkErrorString";
		public const string NetworkErrorFormatString = "NetworkErrorFormatString";


		public const string DialogTitleForSignout = "DialogTitleForSignout";

		public const string DialogForSignoutConfirmTipText = "DialogForSignoutConfirmTipText";

		public const string DialogForSignoutOKText = "DialogForSignoutOKText";
		public const string DialogTitleForCheckNetwork = "DialogTitleForCheckNetwork";
		public const string DialogDescriptionForCheckNetwork = "DialogDescriptionForCheckNetwork"; 
		public const string DialogDescriptionForCheckNetworkFormat = "DialogDescriptionForCheckNetworkFormat";
		public const string DialogDescriptionForCheckNetworkFormat1 = "DialogDescriptionForCheckNetworkFormat1";
		public const string DialogOK = "DialogOK";
		public const string DialogCancel = "DialogCancel";
		public const string DialogTitleForError = "DialogTitleForError";
		public const string DialogDescriptionForError = "DialogDescriptionForError";
		public const string DialogTitleForSaved = "DialogTitleForSaved";
		public const string DialogDescriptionForSaved = "DialogDescriptionForSaved";	
		public const string DialogDescriptionForUnableSave = "DialogDescriptionForUnableSave"; 
		public const string DialogDescriptionForInvalidCredentials = "DialogDescriptionForInvalidCredentials";
		public const string DialogLoading = "DialogLoading";
		public const string DialogSaving = "DialogSaving";		
		public const string DialogTitleForDelete = "DialogTitleForDelete";
		public const string DialogDescriptionForDelete = "DialogDescriptionForDelete";
		public const string DialogConfirmTextForDelete = "DialogConfirmTextForDelete";
		public const string DialogTitleForLoadingProfile = "DialogTitleForLoadingProfile";
		public const string DialogTitleForLoadingContribution = "DialogTitleForLoadingContribution";
		
		public const string ContributionDetailTitleForEditing = "ContributionDetailTitleForEditing";
		public const string ContributionDetailTitleForNew = "ContributionDetailTitleForNew";

		public const string SigninTo = "SigninTo";
		public const string LearnMore = "LearnMore";
		public const string Welcome = "Welcome";
		public const string PageTitleForLogOn = "PageTitleForLogOn";
		public const string PageTitleForSetting = "PageTitleForSetting";
		public const string SignoutButton = "SignoutButton";
		public const string DevToolsInfo = "DevToolsInfo";
		public const string CloseButton = "CloseButton";
		public const string SettingsButton = "SettingsButton";
		public const string CancelButton = "CancelButton";
		public const string AddButton = "AddButton";
		public const string DeleteButton = "DeleteButton";
		public const string SaveButton = "SaveButton";
		public const string LabelForContributionType = "LabelForContributionType";
		public const string LabelForContributionArea = "LabelForContributionArea";
		public const string LabelForContributionDate = "LabelForContributionDate";
		public const string LabelForContributionTitle = "LabelForContributionTitle";
		public const string LabelForURL = "LabelForURL";
		public const string LabelForVisibility = "LabelForVisibility";
		
		public const string LabelForDescription = "LabelForDescription";


		public const string LabelForAwardCategories = "LabelForAwardCategories";
		public const string LabelForFirstAward = "LabelForFirstAward";
		public const string LabelForNumberOfAward = "LabelForNumberOfAward";

		public const string ExpectedCultureIdentifier = "ExpectedCultureIdentifier";

		public const string InvalidUrlMessageText= "InvalidUrlMessageText";		public const string FieldMustbeNumberMessageText= "FieldMustbeNumberMessageText";		public const string RequiredFieldMessageText= "RequiredFieldMessageText";

		//TranslateServices.GetResourceString(CommonConstants.)

		#endregion

		public const string TokenKey = "Token";
		public const string AccessTokenKey = "access_token";
		public const string AuthCodeKey = "auth_code";
		public const string RefreshTokenKey = "refresh_token";
		public const string CurrentUserIdKey = "user_id";
		public const string ProfileCacheListKey = "profile_cache_list";
		public const string ProfileCacheKey = "profile_cache";
		public const string ProfileCacheDateKey = "profile_cache_date";
		public const string ProfilePhotoCacheKey = "profile_photo_cache";
		public const string ProfilePhotoCacheDateKey = "profile_photo_cache_date";

		#region RequestHeaders list
		public const string OcpApimSubscriptionKey = "Ocp-Apim-Subscription-Key";
		public const string OcpApimSubscriptionValue = "9d69252bb303493ba8ff6c9bcfbf9602";
		public const string AuthorizationKey = "Authorization";
		public const string AcceptsKey = "Accepts";
		#endregion

		#region MediaType list
		public const string MediaTypeForJson = "application/json";
		#endregion

		#region Error message for Form Validation.
		public const string HighlightMessageText = "*";
		//public const string RequiredFieldMessageText = "This field is required";
		//public const string InvalidUrlMessageText = "Please enter a valid url";
		//public const string MaxLengthMessageText = "This field exceeded the maximum length";
		//public const string FieldMustbeNumberMessageText = "This is a numerical field.";
		#endregion

		#region Regex Pattern
		public const string UrlPattern = @"^((https?|ftp):\/\/)?(((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-fA-F]{2})|[!\$&amp;'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-fA-F]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)+(\/(([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-fA-F]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-fA-F]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-fA-F]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)|\/|\?)*)?$";
		public const string NumberPattern = @"^[0-9]*$";
		#endregion

		#region Mobile Center
		public const string MobileCenteriOS = "MC_IOS";
		public const string MobileCenterAndroid = "MC_ANDROID";
		public const string MobileCenterUWP = "MC_UWP";
		#endregion

		#region Private Members for LiveIdLogOn 
		public const string Scope = "wl.emails%20wl.basic%20wl.offline_access%20wl.signin";
#if DEBUG
		//public const string ClientId = "LIVE_ID"; 
		//public const string ClientSecret = "LIVE_SECRET"; 
		public const string ClientId = "00000000400FA908"; //<- this is my (Seiya) client key ==> put yours in here 
		public const string ClientSecret = "ta3oGLEAGYP74zNin4I9hm7PvtSNRJJZ";  // <- my (Seiya) secret ), put yours in here 
#else
		public const string ClientId = "000000004818061B"; //<- this is (Micah) client key, put yours in here
		public const string ClientSecret = "XQNsbHbSnd17CNcLYdZcm6i8gz79HA4u";  // <- (Micah) secret ), put yours in here
#endif
		public const string LiveIdLogOnUrlFormatString = @"https://login.live.com/oauth20_authorize.srf?client_id={0}&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_type=code&scope={1}";
		public const string LiveIdSignOutUrlFormatString = "https://login.live.com/oauth20_logout.srf?client_id={0}&redirect_uri=https://login.live.com/oauth20_desktop.srf";
		public const string AccessTokenUrlFormatString = @"https://login.live.com/oauth20_token.srf?client_id={0}&client_secret={1}&redirect_uri=https://login.live.com/oauth20_desktop.srf&grant_type=authorization_code&code=";

		#endregion

		public const string ApiSourceType = "mvp"; //mvp-dev
		public const string BaseUrl = "https://mvpapi.azure-api.net/mvp";// "https://mvpapi.azure-api.net/{APISourceType}";
		public const string ApiUrlOfDeleteContribution = "{0}/api/contributions?id={1}"; //"https://mvpapi.azure-api.net/mvp/api/contributions?id={0}";
		public const string ApiUrlOfDeleteOnlineIdentity = "{0}/api/onlineidentities?id={1}"; //"https://mvpapi.azure-api.net/mvp/api/onlineidentities?id={0}";
		public const string ApiUrlOfGetContributionAreas = "{0}/api/contributions/contributionareas"; //"https://mvpapi.azure-api.net/mvp/api/contributions/contributionareas";
		public const string ApiUrlOfGetContributionTypes = "{0}/api/contributions/Contributiontypes"; //"https://mvpapi.azure-api.net/mvp/api/contributions/Contributiontypes";
		public const string ApiUrlOfGetContributionById = "{0}/api/contributions/{1}"; //"https://mvpapi.azure-api.net/mvp/api/contributions/{0}";
		public const string ApiUrlOfGetContributions = "{0}/api/contributions/{1}/{2}"; //"https://mvpapi.azure-api.net/mvp/api/contributions/{0}/{1}";// https://mvpapi.azure-api.net/mvp/api/contributions/{offset}/{limit}
		public const string ApiUrlOfGetMvpProfile = "{0}/api/profile"; //"https://mvpapi.azure-api.net/mvp/api/profile";
		public const string ApiUrlOfGetMvpProfileById = "{0}/api/profile/{1}"; //"https://mvpapi.azure-api.net/mvp/api/profile/{0}";//https://mvpapi.azure-api.net/mvp/api/profile/{mvpid}
		public const string ApiUrlOfGetMvpProfileImage = "{0}/api/profile/photo"; //"https://mvpapi.azure-api.net/mvp/api/profile/photo";
		public const string ApiUrlOfGetOnlineIdentities = "{0}/api/onlineidentities"; //"https://mvpapi.azure-api.net/mvp/api/onlineidentities";
		public const string ApiUrlOfGetOnlineIdentitiesByNominationsId = "{0}/api/onlineidentities/{1}"; //"https://mvpapi.azure-api.net/mvp/api/onlineidentities/{0}";
		public const string ApiUrlOfGetOnlineIdentityById = "{0}/api/onlineidentities/{1}"; //"https://mvpapi.azure-api.net/mvp/api/onlineidentities/{0}";
		public const string ApiUrlOfPostContribution = "{0}/api/contributions"; //"https://mvpapi.azure-api.net/mvp/api/contributions";
		public const string ApiUrlOfPostOnlineIdentity = "{0}/api/onlineidentities"; //"https://mvpapi.azure-api.net/mvp/api/onlineidentities";
		public const string ApiUrlOfPutContribution = "{0}/api/contributions"; //"https://mvpapi.azure-api.net/mvp/api/contributions";
		public const string ApiUrlOfPutOnlineIdentity = "{0}/api/onlineidentities"; //"https://mvpapi.azure-api.net/mvp/api/onlineidentities";
		public const string LogOffUrl = "https://mvp.microsoft.com/account/logout";

		public const string DefaultPhoto = "/9j/4QnhRXhpZgAATU0AKgAAAAgABwESAAMAAAABAAEAAAEaAAUAAAABAAAAYgEbAAUAAAABAAAAagEoAAMAAAABAAIAAAExAAIAAAAdAAAAcgEyAAIAAAAUAAAAj4dpAAQAAAABAAAApAAAANAADqYAAAAnEAAOpgAAACcQQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKQAyMDE2OjEwOjIwIDE3OjMwOjQ0AAAAA6ABAAMAAAABAAEAAKACAAQAAAABAAAAyKADAAQAAAABAAAAyAAAAAAAAAAGAQMAAwAAAAEABgAAARoABQAAAAEAAAEeARsABQAAAAEAAAEmASgAAwAAAAEAAgAAAgEABAAAAAEAAAEuAgIABAAAAAEAAAirAAAAAAAAAEgAAAABAAAASAAAAAH/2P/tAAxBZG9iZV9DTQAB/+4ADkFkb2JlAGSAAAAAAf/bAIQADAgICAkIDAkJDBELCgsRFQ8MDA8VGBMTFRMTGBEMDAwMDAwRDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAENCwsNDg0QDg4QFA4ODhQUDg4ODhQRDAwMDAwREQwMDAwMDBEMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwM/8AAEQgAoACgAwEiAAIRAQMRAf/dAAQACv/EAT8AAAEFAQEBAQEBAAAAAAAAAAMAAQIEBQYHCAkKCwEAAQUBAQEBAQEAAAAAAAAAAQACAwQFBgcICQoLEAABBAEDAgQCBQcGCAUDDDMBAAIRAwQhEjEFQVFhEyJxgTIGFJGhsUIjJBVSwWIzNHKC0UMHJZJT8OHxY3M1FqKygyZEk1RkRcKjdDYX0lXiZfKzhMPTdePzRieUpIW0lcTU5PSltcXV5fVWZnaGlqa2xtbm9jdHV2d3h5ent8fX5/cRAAICAQIEBAMEBQYHBwYFNQEAAhEDITESBEFRYXEiEwUygZEUobFCI8FS0fAzJGLhcoKSQ1MVY3M08SUGFqKygwcmNcLSRJNUoxdkRVU2dGXi8rOEw9N14/NGlKSFtJXE1OT0pbXF1eX1VmZ2hpamtsbW5vYnN0dXZ3eHl6e3x//aAAwDAQACEQMRAD8A7ZJJJJSkkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJT//0O2SSSSUpJJJJSkkkklKSSSSUpJJPsdztMfCElLJJy1wEkGDwUySlJJJJKUkkkkpSSSSSlJJJJKf/9HtkkkklKSSSSUpJJJJSlV6j1PC6Zj+vmP2gyK2N1e8jtW3/v7vYiZuXRg4luXkGKqW7iO7j+bW3+W9yw+jdNs6pf8At7rDfUfbrh4zh7GMB9j9h/wf+ir/AD/55JS7Mj60dZAfihvScF30bHfzrh+83T1P8xtbP+ETj6oiyHZXU8q6z84tIaJ/khxsXQEkmTqUklOB/wA1cij3dP6tk0v7eodw+ew/98UP2z1vo7ms67QMnFJ2jNoA/wCltDWu/qPbVYuiTOa17HMe0PY8Q9jhLSPBzSkphj5FGVQzIxrBbTZ9B7ePh/W/kIi5m6t/1W6g3Kol3Rcx22+qZ9J3iJ/cb/Nf6Sv9GumkEAtIc0iWuHBB4ISUpJJJJSkkkklKSSSSU//S7ZJJJJSkkkklKSSSSU8/9Ywc/qXTeiAwy5/r5EaexvH/AEW2roIaAGtG1rQA1o4AGjW/5qwQN314O/T0sP8ARg95b2/z3reSUpJJJJSkkkklIM7Crz8K7Cs+jc2AfBw1qf8A2XrN+qWW+/o7aLf53Ce7Hf5Aa1D+y32LaHI+KwPq2AzqXW6ma1tyJafMufpp+6kp3kkkklKSSSSUpJJJJT//0+2SSSSUpJJJJSkkkklOB1MjB+tXTc5+lOTW7Ge7sHas9zv+u1rfIgweQs/rvS/2p01+O3S9h9THdx72z7f6tjfYh/V/q/7SxPTvlmfijZk1u0cY9vrbf5X+E/4RJTqJJJJKUkkkkpZ1jKmutsO2uoF7yezWjc5YX1Prc/Dys94IOdkPeJ5gbj/1b031jzbcu1n1e6ed2VkkfanDiuse7Y8/+CXf5n+FW1i41OJi1YlP81Q0MbPfxf8A1nu9ySkqSSSSlJJJJKUkkkkp/9TtkkkklKSSSSUpJJJJSlj9X6FZkZA6n0y37L1Nn587W2f8Y7/Sf+fv8ItLLzMXBoORl2CmoaBx7n92to91jv6iwT1zrnV3ur6FjehQ0wcu0CR/af8Aoa//AAS1JSej60eg8Y3XcazByRzYGE1O/l7R72f2PVYtOvq3SbGh7M7HLTwTa1n/AEbTW5Y7PqldkOFnVuo25L/3a5gf1LLt23/rdKtM+qPQG/SpstPi6xw18fZtSU27+tdGx27rc2nyDHCwn4Np9RZdvXepdVJxvq/jPYw6WZ9w2hg77J9tf/n3/gkd/wBT+hun02W0+bLCY/7cD1WP1b6tg+/o3UnADUUWna0nw/Pod/bYxJTp9G6Lj9KqdDvWyrv5/Jdy4/S2M/4Pd/24tBc7j/WbJw7xifWDGONYeMhjTtIH5z6/o2N/4ShdBXZXbW22p4sreJY9plpB7tckpkkkkkpSSSSSlJJJJKf/1e2SSSSUpJJJJSlV6n1LG6XiOyskyAdtdY0c937g/d/ee/8AMYrXxMDkk9gO65fFZ/zl63ZmXCel4J2U1nh5n2N/67t9a/8Akemkpl0/pGV1u5vVuuE+i7XGw2y0bD/57x//AAa9dKxrWMbWxrWMYIYxgDWgD9xrfoqRMn+7RMkpSSSSSlJJJJKRZOLjZlBx8qtt1J/Md2P7zH/Sqf8Aymrmn1531UyRbUXZfRrnQ5h1cwn/AKLLv3LGfo8ldUoW0030vovYLKbRssYeCD/37+UkpVN9ORTXkUOFlNrQ6t44IP8A35v56mua6M+3ovWrOhXu3YuSfUw7D4n3M/7d+hY3/TLpfw8klKSSSSUpJJJJT//W7ZJJJJSkkkklOV9Z804fRL3NMPvihh7+7+c/8Daj9CwRgdJxseIeW+rb/XsAcf8Ao7VlfXFwe/peKSALrySCYn6DP/JLpHgBxjgGB8BoElLJJJJKUkkkkpSSSSSlJJJJKcH644rndPq6hVpfg2Ahw/dcR/1NuxbOLkjLxaMscZFbbPmR7h/noPV6xb0nNrPeh5HxA3NVP6pXC76vYxBDtheyR5OJ/wC/JKddJJJJSkkkklP/1+2SSSSUpJJJJSxDTEtBI4JAJH9Wfop0kklKSSSSUpJJJJSkkkklKSSSSUr46jwKQAAgANHgAAPuakkkpSSSSSlJJJJKf//Q7ZJJJJSkkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJT//2f/tEcJQaG90b3Nob3AgMy4wADhCSU0EJQAAAAAAEAAAAAAAAAAAAAAAAAAAAAA4QklNBDoAAAAAANcAAAAQAAAAAQAAAAAAC3ByaW50T3V0cHV0AAAABQAAAABQc3RTYm9vbAEAAAAASW50ZWVudW0AAAAASW50ZQAAAABJbWcgAAAAD3ByaW50U2l4dGVlbkJpdGJvb2wAAAAAC3ByaW50ZXJOYW1lVEVYVAAAAAEAAAAAAA9wcmludFByb29mU2V0dXBPYmpjAAAABWghaDeLvn9uAAAAAAAKcHJvb2ZTZXR1cAAAAAEAAAAAQmx0bmVudW0AAAAMYnVpbHRpblByb29mAAAACXByb29mQ01ZSwA4QklNBDsAAAAAAi0AAAAQAAAAAQAAAAAAEnByaW50T3V0cHV0T3B0aW9ucwAAABcAAAAAQ3B0bmJvb2wAAAAAAENsYnJib29sAAAAAABSZ3NNYm9vbAAAAAAAQ3JuQ2Jvb2wAAAAAAENudENib29sAAAAAABMYmxzYm9vbAAAAAAATmd0dmJvb2wAAAAAAEVtbERib29sAAAAAABJbnRyYm9vbAAAAAAAQmNrZ09iamMAAAABAAAAAAAAUkdCQwAAAAMAAAAAUmQgIGRvdWJAb+AAAAAAAAAAAABHcm4gZG91YkBv4AAAAAAAAAAAAEJsICBkb3ViQG/gAAAAAAAAAAAAQnJkVFVudEYjUmx0AAAAAAAAAAAAAAAAQmxkIFVudEYjUmx0AAAAAAAAAAAAAAAAUnNsdFVudEYjUHhsQFgAAAAAAAAAAAAKdmVjdG9yRGF0YWJvb2wBAAAAAFBnUHNlbnVtAAAAAFBnUHMAAAAAUGdQQwAAAABMZWZ0VW50RiNSbHQAAAAAAAAAAAAAAABUb3AgVW50RiNSbHQAAAAAAAAAAAAAAABTY2wgVW50RiNQcmNAWQAAAAAAAAAAABBjcm9wV2hlblByaW50aW5nYm9vbAAAAAAOY3JvcFJlY3RCb3R0b21sb25nAAAAAAAAAAxjcm9wUmVjdExlZnRsb25nAAAAAAAAAA1jcm9wUmVjdFJpZ2h0bG9uZwAAAAAAAAALY3JvcFJlY3RUb3Bsb25nAAAAAAA4QklNA+0AAAAAABAAYAAAAAEAAgBgAAAAAQACOEJJTQQmAAAAAAAOAAAAAAAAAAAAAD+AAAA4QklNBA0AAAAAAAQAAAB4OEJJTQQZAAAAAAAEAAAAHjhCSU0D8wAAAAAACQAAAAAAAAAAAQA4QklNJxAAAAAAAAoAAQAAAAAAAAACOEJJTQP1AAAAAABIAC9mZgABAGxmZgAGAAAAAAABAC9mZgABAKGZmgAGAAAAAAABADIAAAABAFoAAAAGAAAAAAABADUAAAABAC0AAAAGAAAAAAABOEJJTQP4AAAAAABwAAD/////////////////////////////A+gAAAAA/////////////////////////////wPoAAAAAP////////////////////////////8D6AAAAAD/////////////////////////////A+gAADhCSU0EAAAAAAAAAgABOEJJTQQCAAAAAAAEAAAAADhCSU0EMAAAAAAAAgEBOEJJTQQtAAAAAAAGAAEAAAACOEJJTQQIAAAAAAAQAAAAAQAAAkAAAAJAAAAAADhCSU0EHgAAAAAABAAAAAA4QklNBBoAAAAAAz8AAAAGAAAAAAAAAAAAAADIAAAAyAAAAAVnKmgHmJgALQAxAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAADIAAAAyAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAABAAAAABAAAAAAAAbnVsbAAAAAIAAAAGYm91bmRzT2JqYwAAAAEAAAAAAABSY3QxAAAABAAAAABUb3AgbG9uZwAAAAAAAAAATGVmdGxvbmcAAAAAAAAAAEJ0b21sb25nAAAAyAAAAABSZ2h0bG9uZwAAAMgAAAAGc2xpY2VzVmxMcwAAAAFPYmpjAAAAAQAAAAAABXNsaWNlAAAAEgAAAAdzbGljZUlEbG9uZwAAAAAAAAAHZ3JvdXBJRGxvbmcAAAAAAAAABm9yaWdpbmVudW0AAAAMRVNsaWNlT3JpZ2luAAAADWF1dG9HZW5lcmF0ZWQAAAAAVHlwZWVudW0AAAAKRVNsaWNlVHlwZQAAAABJbWcgAAAABmJvdW5kc09iamMAAAABAAAAAAAAUmN0MQAAAAQAAAAAVG9wIGxvbmcAAAAAAAAAAExlZnRsb25nAAAAAAAAAABCdG9tbG9uZwAAAMgAAAAAUmdodGxvbmcAAADIAAAAA3VybFRFWFQAAAABAAAAAAAAbnVsbFRFWFQAAAABAAAAAAAATXNnZVRFWFQAAAABAAAAAAAGYWx0VGFnVEVYVAAAAAEAAAAAAA5jZWxsVGV4dElzSFRNTGJvb2wBAAAACGNlbGxUZXh0VEVYVAAAAAEAAAAAAAlob3J6QWxpZ25lbnVtAAAAD0VTbGljZUhvcnpBbGlnbgAAAAdkZWZhdWx0AAAACXZlcnRBbGlnbmVudW0AAAAP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YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNS1jMDE0IDc5LjE1MTQ4MSwgMjAxMy8wMy8xMy0xMjowOToxNSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTYtMTAtMjBUMTc6MzA6MjArMDg6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE2LTEwLTIwVDE3OjMwOjQ0KzA4OjAwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDE2LTEwLTIwVDE3OjMwOjQ0KzA4OjAwIiBkYzpmb3JtYXQ9ImltYWdlL2pwZWciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDo1Nzc2NzJmOS0xZGM2LWUyNGItOWUzYy1hYmQ4ZWE1NGU3NDIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6OTFiZTc4MjctY2Y0Yy1lNDQ5LTgxNzQtNTdmODQyYjg2YjExIiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6OTFiZTc4MjctY2Y0Yy1lNDQ5LTgxNzQtNTdmODQyYjg2YjExIj4gPHBob3Rvc2hvcDpEb2N1bWVudEFuY2VzdG9ycz4gPHJkZjpCYWc+IDxyZGY6bGk+MkY0OEU0RTEyQThCNUM2ODMxN0MzOThDRjU1MzJFQUI8L3JkZjpsaT4gPC9yZGY6QmFnPiA8L3Bob3Rvc2hvcDpEb2N1bWVudEFuY2VzdG9ycz4gPHhtcE1NOkhpc3Rvcnk+IDxyZGY6U2VxPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY3JlYXRlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDo5MWJlNzgyNy1jZjRjLWU0NDktODE3NC01N2Y4NDJiODZiMTEiIHN0RXZ0OndoZW49IjIwMTYtMTAtMjBUMTc6MzA6MjArMDg6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cykiLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249ImNvbnZlcnRlZCIgc3RFdnQ6cGFyYW1ldGVycz0iZnJvbSBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wIHRvIGltYWdlL2pwZWciLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249InNhdmVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjU3NzY3MmY5LTFkYzYtZTI0Yi05ZTNjLWFiZDhlYTU0ZTc0MiIgc3RFdnQ6d2hlbj0iMjAxNi0xMC0yMFQxNzozMDo0NCswODowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8L3JkZjpTZXE+IDwveG1wTU06SGlzdG9yeT4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+ICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAg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";
		public const string OkResult = "OK";

		public const string LearnMoreUrl = "https://mvp.microsoft.com/";
		
	}
}
