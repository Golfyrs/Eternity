using System.Collections.Generic;
using Eternity.Core;
using Eternity.Unity.Common.Components.Weaving;
using Eternity.Unity.Core.Patterns;
using UnityEngine;

namespace Eternity.Unity.Core.GameWorld
{
    public class Players : Weaver<World>
    {
        public GameObject PlayerTemplate;
        
        protected override void Weave(World idea)
        {
            PlayerTemplate.SetActive(false);
            
            idea.Players().OnNext(Spawn);
            
            Spawn(idea.Players().Current);

            idea.Spawn("Test1", 2, 2);
        }

        private void Spawn(IEnumerable<Player> players)
        {
            foreach (Transform child in transform)
                DestroyImmediate(child.gameObject, true);

            foreach (var player in players)
                Spawn(player);
        }

        private void Spawn(Player player)
        {
            var playerGameObject = Instantiate(PlayerTemplate, transform);
            var pattern = playerGameObject.GetComponent<PlayerPattern>();

            pattern.Player = player;

            playerGameObject.SetActive(true);
        }
    }
}