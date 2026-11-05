using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Question2.Models;

namespace Question2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadMenuItems();
        }

        private void LoadComboBoxes()
        {
            using (var context = new Prn2125Context())
            {
                var categories = context.Categories.ToList();
                var filterCategories = new List<Category>
                {
                    new Category { CategoryId = 0, CategoryName = "All" }
                };
                filterCategories.AddRange(categories);
                cboFilterCategory.ItemsSource = filterCategories;
                cboFilterCategory.SelectedIndex = 0;

                cboAddCategory.ItemsSource = categories;
                cboAddCategory.SelectedIndex = -1;

                var kitchens = context.Kitchens.ToList();
                var filterKitchens = new List<Kitchen>
                {
                    new Kitchen { KitchenId = 0, KitchenName = "All" }
                };
                filterKitchens.AddRange(kitchens);
                cboFilterKitchen.ItemsSource = filterKitchens;
                cboFilterKitchen.SelectedIndex = 0;

                cboAddKitchen.ItemsSource = kitchens;
                cboAddKitchen.SelectedIndex = -1;
            }
        }

        private void LoadMenuItems()
        {
            using (var context = new Prn2125Context())
            {
                var query = context.MenuItems
                    .Include(m => m.Category)
                    .Include(m => m.Kitchen)
                    .AsQueryable();

                if (cboFilterCategory.SelectedValue is int categoryId && categoryId > 0)
                {
                    query = query.Where(m => m.CategoryId == categoryId);
                }

                if (cboFilterKitchen.SelectedValue is int kitchenId && kitchenId > 0)
                {
                    query = query.Where(m => m.KitchenId == kitchenId);
                }

                var menuItems = query
                    .Select(m => new
                    {
                        m.MenuItemId,
                        m.ItemName,
                        m.ItemCode,
                        m.Price,
                        m.PreparationMinutes,
                        CategoryName = m.Category.CategoryName,
                        KitchenName = m.Kitchen.KitchenName
                    })
                    .ToList();

                dgMenuItems.ItemsSource = menuItems;
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadMenuItems();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cboFilterCategory.SelectedIndex = 0;
            cboFilterKitchen.SelectedIndex = 0;
            LoadMenuItems();
        }

        private void btnAddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            string itemCode = txtItemCode.Text.Trim();
            string priceText = txtPrice.Text.Trim();
            string prepMinutesText = txtPreparationMinutes.Text.Trim();

            if (string.IsNullOrWhiteSpace(itemName) ||
                string.IsNullOrWhiteSpace(itemCode) ||
                string.IsNullOrWhiteSpace(priceText) ||
                string.IsNullOrWhiteSpace(prepMinutesText) ||
                cboAddCategory.SelectedValue == null ||
                cboAddKitchen.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields and select both Category and Kitchen.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price) ||
                !int.TryParse(prepMinutesText, out int prepMinutes))
            {
                MessageBox.Show("Price and PreparationMinutes must be numeric values.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new Prn2125Context())
            {
                var newItem = new MenuItem
                {
                    ItemName = itemName,
                    ItemCode = itemCode,
                    Price = price,
                    PreparationMinutes = prepMinutes,
                    CategoryId = (int)cboAddCategory.SelectedValue,
                    KitchenId = (int)cboAddKitchen.SelectedValue
                };

                context.MenuItems.Add(newItem);
                context.SaveChanges();
            }

            LoadMenuItems();
            ClearForm();
        }

        private void btnClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtItemName.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtPreparationMinutes.Text = string.Empty;
            cboAddCategory.SelectedIndex = -1;
            cboAddKitchen.SelectedIndex = -1;
        }
    }
}
