$year = 2015
$url = "https://adventofcode.com/$year"
$cookieFile = '.\aocCookie'

# Put this at the top of the README.md
$header = @"
# Advent of Code 2015
- My attempt to catch up on all the Advents of Code.
- Starting this project in winter 2023.

"@

# =====================================================================================================================
# Functions 
# =====================================================================================================================
# Get the title of the specified day
function Get-DayTitle {
    param ($day)

    $dayUrl = "$url/day/$day"
    $dayResp = Invoke-WebRequest -Uri $dayUrl
    $content = $dayResp.Content 
    $content -Match '--- Day (.*) ---' | Out-Null
    $splits = $matches[1] -Split ': '
    $title = $splits[1]
    $title
}

# Generate README.md
function Write-ReadMeFile {
    $stars = Get-StarCount
    $progress = "## Progress: ![Progress](https://progress-bar.dev/$stars/?scale=50&title=StarsCollected&width=400&suffix=/50)`r`n"

    $readme = $header + $progress + ($sortedDays | ConvertTo-MarkDownTable) 
    Set-Content -Path '.\README.md' -Value $readme
}

# count the stars completed, one for each part of each day
function Get-StarCount {
    $stars = 0
    $sortedDays | ForEach-Object {
        if ($_.Part1) { $stars++ } 
        if ($_.Part2) { $stars++ }
    }
    return $stars
}

# create markdown table
function ConvertTo-MarkDownTable {
    [CmdletBinding()] param(
        [Parameter(Position = 0, ValueFromPipeLine = $True)] $InputObject
    )
    Begin {
        "| Day | Status | Source | Solution Description |`r`n"
        "| - | - | - | - |`r`n"
    }
    Process {
        $dayLink = '[Day ' + ([string]$_.Day).PadLeft(2, '0') + ':  ' + $_.Title + '](' + $url + '/day/' + $_.Day + ')'
        $solLink = '[Solution](./Day' + ([string]$_.Day).PadLeft(2, '0') + '/Program.cs)'
        if ($_.Part1) { $pt1 = ':star:' } else { $pt1 = '' }
        if ($_.Part2) { $pt2 = ':star:' } else { $pt2 = '' }
        "| $dayLink | $pt1$pt2 | $solLink | Add descriptive text here |`r`n"
    }
    End {}
}

# =====================================================================================================================
# Script start
# =====================================================================================================================
# Read saved status file
if (Test-Path -Path 'dayStatus.json') {
    $localStatus = (Get-Content "DayStatus.json" -raw) | ConvertFrom-Json
} 

# Read cookie file (ex. session="5423819...")
$cookie = Get-Content -Path $cookieFile -TotalCount 1
$parts = $cookie -Split '='

# Make a session cookie object
#$sessCookie = New-Object System.Net.Cookie
$sessCookie = [System.Net.Cookie]::new()
$sessCookie.Name = $parts[0]
$sessCookie.Value = $parts[1]
$sessCookie.Domain = "adventofcode.com"

# Make a WebRequestSession object and add the cookie
$wrs = [Microsoft.PowerShell.Commands.WebRequestSession]::new()
$wrs.Cookies.Add($sessCookie)

# Get the main page
$iwrResp = Invoke-WebRequest -Uri $url -WebSession $wrs

# Grab all the links with aria-label, there should be up to 25 of them
$aLabel = 'aria-label'
$status = $iwrResp.Links | Select-Object -Property $aLabel | Where-Object -Property $aLabel

# Get day and star information from the main page
$dayList = [System.Collections.ArrayList]::new()
$status | ForEach-Object {
    # Get day number
    $_.($aLabel) -match '(\d+)' | Out-Null
    $day = [int]$Matches[0]

    # Get star status for part1 and part2
    # $part1 should always be true if $part2 is true
    $part2 = $_.$aLabel -match '(two)'
    if ($part2) {
        $part1 = $true
    }
    else {
        $part1 = $_.$aLabel -match '(one)'
    }
    
    # If any stars on this day, build day object
    if (($part1 -or $part2) -eq $true) {

        # Get day info from local file and use its title
        if (Test-Path variable:localStatus) {
            $localDay = $localStatus | Where-Object -Property Day -eq $day
            if ([string]::IsNullOrEmpty($localDay.Title)) {
                $title = $localDay.Title
            }
            else {
                $title = Get-DayTitle -Day $day
            }
        }
        else {
            $title = Get-DayTitle -Day $day
        }

        # create object for the day and add to list
        $tmp = [PSCustomObject]@{
            Day   = $day
            Part1 = $part1
            Part2 = $part2
            Title = $title
            Link  = '.\Day' + ([string]$day).PadLeft(2, '0') + '\Program.cs'
        }
        $dayList.Add($tmp) | Out-Null
    }
}
# List may not be in order (AoC2015)
$sortedDays = $dayList | Sort-Object -Property Day

# Write the README.md file and save current status to json file
Write-ReadMeFile
$json = ConvertTo-Json -InputObject $sortedDays
$json | Out-File DayStatus.json
