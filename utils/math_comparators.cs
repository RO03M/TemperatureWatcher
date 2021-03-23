namespace temperatures.src.utils {
    class math_comparators {
        public static bool InRange(float numberToCompare, float range1, float range2) {
            float lowerValue = (range1 > range2 ? range2 : range1);
            float biggerValue = (range1 > range2 ? range1 : range2);
            if (numberToCompare > lowerValue && numberToCompare < biggerValue) return true;
            return false;
        }
    }
}
