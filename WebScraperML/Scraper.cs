using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

class Scraper
{
    static async Task Main(string[] args)
    {
        // Create an instance of Playwright. This initializes the Playwright driver.
        using var playwright = await Playwright.CreateAsync();

        // Launch a Chromium browser instance in headless mode (no UI)
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
            Headless = true
        });
        
        
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://www.it-kanalen.se/");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        var pageTitle = await page.TitleAsync();
        Console.WriteLine($"Sidtitel: {pageTitle}");
        var links = page.Locator("a");
        int count = await links.CountAsync();
        
        for (int i = 0; i < count; i++) {
            var text = await links.Nth(i).InnerTextAsync();
            var href = await links.Nth(i).GetAttributeAsync("href");
            Console.WriteLine($"{text} -> {href}");
        }
    }
}
