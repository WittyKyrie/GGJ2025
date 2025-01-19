namespace Util
{
    public static class SoundEffects
    {
        public const string InGameAmbBar = "Play_sfx_amb_bar_ingame";
        public const string OutGameAmbBar = "Play_sfx_amb_bar_menu";
        public const string Click = "Play_sfx_click_start";
        public const string InGameBgm = "Play_bgm_ingame";
        
        public const string NormalClick = "Play_sfx_click_normal"; // 通用点击
        public const string CutScene = "Play_sfx_cut_sence"; // 开局过场音效
        public const string DrinkingRound = "Play_sfx_drinking_round"; // 喝酒回合音效（进入喝酒回合就播）
        public const string PouringBeer = "Play_sfx_pouring_beer"; // 倒啤酒音效
        public const string RoundNotice = "Play_sfx_round_notice"; // 回合开始提示音
        public const string StopPouringBeer = "Stop_sfx_pouring_beer"; // 本回合内停止倒酒事件
    }
}