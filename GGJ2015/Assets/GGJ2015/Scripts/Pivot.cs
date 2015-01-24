using System;
using System.Collections.Generic;


namespace Assets.GGJ2015.Scripts {
    [Serializable]
    public class Pivot {
        public string Id;
        public string Description;
        public PivotGraphics Graphics;
        public List<Choice> Choices;
    }
}
