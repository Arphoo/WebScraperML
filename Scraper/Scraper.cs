using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace WebScraperML;
class Scraper
{
    static async Task Main(string[] args)
    {
        // Create an instance of Playwright. This initializes the Playwright driver.
        using var playwright = await Playwright.CreateAsync();

        // Launch a Chromium browser instance in headless mode (no UI)
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        // Open a new browser page (tab)
        var page = await browser.NewPageAsync();

        // Navigate to the website
        await page.GotoAsync("https://www.it-kanalen.se/");

        // Wait until the network is idle, meaning most JavaScript requests have finished
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Get the page title and print it
        var pageTitle = await page.TitleAsync();
        Console.WriteLine($"Sidtitel: {pageTitle}");  // "Sidtitel" = Page Title in Swedish

        // Select all <a> elements on the page using a Locator
        var links = page.Locator("a");

        // Count how many <a> elements were found
        int count = await links.CountAsync();
        Console.WriteLine($"Hittade {count} länkar"); // "Hittade X länkar" = Found X links

        // Loop through each link
        for (int i = 0; i < count; i++)
        {
            // This line is unfinished and currently will cause a compile error:
            // if (!string.Equals()) { ... }
            // You probably wanted to skip empty links or filter certain URLs.

            // Get the inner text of the link
            var text = await links.Nth(i).InnerTextAsync();

            // Get the href attribute of the link
            var href = await links.Nth(i).GetAttributeAsync("href");

            // Print the link text and href to the console
            Console.WriteLine($"{text} -> {href}");
        }

        // Print "Klar." to indicate the scraper finished
        Console.WriteLine("Klar."); // "Klar" = Done
    }
}
