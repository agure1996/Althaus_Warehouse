namespace Althaus_Warehouse.Models.Entities
{
    /// <summary>
    /// Enum to hold the different item categories
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Perishable Goods 0-5
        /// </summary>
        Dairy,
        Meat,
        Seafood,
        Fruits,
        Vegetables,
        Beverages,

        /// <summary>
        /// Non-Perishable Goods 6-13
        /// </summary>
        Electronics,
        Furniture,
        Clothing,
        Toys,
        Stationery,
        Books,
        Tools,
        CleaningSupplies,

        /// <summary>
        /// Household & Personal Care 14-16
        /// </summary>
        PersonalCare,
        HouseholdAppliances,
        Cosmetics,

        /// <summary>
        /// Grocery Items 17-21
        /// </summary>
        Grocery,
        Snacks,
        BakingSupplies,
        Spices,
        Grains,

        /// <summary>
        /// Office Equipment 22-24
        /// </summary>
        OfficeSupplies,
        Computers,
        Monitors,

        /// <summary>
        /// Miscellaneous 25-29
        /// </summary>
        SportsEquipment,
        Automotive,
        HealthAndWellness,
        Jewelry,
        Footwear,
    }


}
