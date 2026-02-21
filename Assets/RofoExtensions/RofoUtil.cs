using System.IO;

namespace Extensions
{
	public static partial class RofoUtil
	{
		#region PrefKeys
		public const string PKEY_BYPASS = "Bypass";
		public const string PKEY_LEVEL = "Level";
		public const string PKEY_MONEY = "Money";
		public const string PKEY_VIBRATION = "Vibration";
		public const string PKEY_SOUND = "Sound";
		public const string PKEY_MUSIC = "Music";
		public const string GAME_EXTENSIONS = "Extensions";
		#endregion PrefKeys

		#region LevelKeys
		public const string LEVEL = "Level";
		public const string LEVELS = "Levels";
		public static string LEVELS_PATH = Path.Combine("Levels", "Level ");
		public const string MAIN_SCENE = "Main";
		public const string PLAYER_POINT = "PlayerPoint";

		public const string DEFAULT_LEVEL_ORDER = "1-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-20";
		public const string DEFAULT_LOOP_LEVEL_ORDER = "3-2-1-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-20";
		#endregion LevelKeys

		#region UIKeys
		public static string UI_OFF_TEXT = "Off";
		public static string UI_ON_TEXT = "On";
		public static string UI_RESTORE_PURCHASE_TEXT = "Restore Purchases";
		#endregion UIKeys

		#region AnimationKeys
		public const string ANIM_RUN = "Run";
		public const string ANIM_IDLE = "Idle";
		public const string ANIM_MODE = "AnimMode";


		public const string SHOOT_STATE = "Shoot";
		public const string RELOAD_STATE = "Reload";
		public const string SCOPE_IN_STATE = "ScopeIn";
		public const string SCOPE_OUT_STATE = "ScopeOut";

		public const string SHOOT_SPEED_PARAM = "ShootSpeed";
		public const string RELOAD_SPEED_PARAM = "ReloadSpeed";
		public const string SCOPE_IN_SPEED_PARAM = "ScopeInSpeed";
		public const string SCOPE_OUT_SPEED_PARAM = "ScopeOutSpeed";


		public const string SHOOT_ANIM_PARAM = "Shoot";
		public const string RELOAD_ANIM_PARAM = "Reload";
		public const string SCOPE_ANIM_PARAM = "Scope";



		#endregion AnimationKeys

		#region TagKeys
		public const string TAG_UNTAGGED = "Untagged";
		public const string TAG_PLAYER = "Player";
		public const string TAG_FINISH = "Finish";
		public const string TAG_RESPAWN = "Respawn";
		public const string TAG_EDITOR_ONLY = "EditorOnly";
		public const string TAG_MAIN_CAMERA = "MainCamera";
		public const string TAG_GAME_CONTROLLER = "GameController";
		#endregion TagKeys

		#region LayerKeys
		public const string LAYER_DEFAULT = "Default";
		public const string LAYER_TRANSPARENT_FX = "TransparentFX";
		public const string LAYER_IGNORE_RAYCAST = "Ignore Raycast";
		public const string LAYER_WATER = "Water";
		public const string LAYER_UI = "UI";

		public const string LAYER_FPS_PLAYER = "FPSPlayer";
		public const string LAYER_PLAYER_HIT = "PlayerHit";
		public const string LAYER_ENEMY_HIT = "EnemyHit";
		public const string LAYER_DONT_RENDER = "DontRender";

		#endregion LayerKeys

		#region JSON
		public const string SAVED_JSONS = "SavedJson";
		public const string EXT_JSON = ".json";
		#endregion JSON

		#region OTHER
		public const string EDITOR = "Editor";
		public const string DEFAULT = "Default";

		public const string TOAST_MESSAGE_AD = "Ad is not ready!";
		public const string TOAST_MESSAGE_AD_FAIL = "Ad is not completed!";
		public const string TOAST_MESSAGE_RESTORE_SUCCESS = "Restored Succesfully!";
		public const string TOAST_MESSAGE_MAX_LEVEL = "Max Level!";


		public const string TRY_AGAIN_LATER = "Try again later";

		#endregion OTHER

		#region ShaderProperties
		public const string PROP_EMISSION_COLOR = "_EmissionColor";
		public const string PROP_EMISSION_TEXTURE = "_EmissionMap";
		public const string PROP_MAIN_TEXTURE = "_MainTex";
		public const string PROP_COLOR = "_Color";
		public const string PROP_ALBEDO = "_MainTex";
		public const string PROP_SMOOTHNESS = "_Glossiness";
		public const string PROP_METALLIC = "_Metallic";
		public const string PROP_ALPHA_CUTOFF = "_Cutoff";
		#endregion ShaderProperties
	}

}

