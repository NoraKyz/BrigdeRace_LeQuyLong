using UnityEngine;

namespace Utils
{
    public static class Constants
    {
        // Input
        public const float MinSwipeDistance = 0.01f;
        
        // Brick
        public static readonly Vector3 BridgeBrickSize = new Vector3(2f,0.3f,2f);
        public static readonly Vector3 CharacterBrickSize = new Vector3(0.75f,0.2f,0.3f);
        public const float RespawnPlatformBrickTime = 5f;
        
        // Character
        public const float StunTime = 1.5f;
        
        // Game play
        public const int MaxLevel = 2;
        public const float TimeToStartGame = 3f;
    }
}
