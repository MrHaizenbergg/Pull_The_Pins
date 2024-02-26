using UnityEngine;



namespace ShopMechanics
{
    [CreateAssetMenu(fileName = ("CharactershopOB"),menuName = ("Shopping/Character Shop"),order = 1)]
    public class CharacterShopOB : ScriptableObject
    {
        public Character[] characters;

        public int GetAmountCharacter() => characters.Length;

        public Character GetCharacter(int index) => characters[index];
    }
}
