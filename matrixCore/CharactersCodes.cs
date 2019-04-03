namespace matrixCore
{
    public class CharactersCodes
    {
        public readonly uint MinCharCode;
        public readonly uint MaxCharCode;
        public readonly uint SpaceCode;

        public CharactersCodes(uint minCharCode, uint maxCharCode, uint spaceCode)
        {
            MinCharCode = minCharCode;
            MaxCharCode = maxCharCode;
            SpaceCode = spaceCode;
        }
    }
}