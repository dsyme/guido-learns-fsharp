module Api

open DataAccess
open FSharp.Data.UnitSystems.SI.UnitNames
open Shared

let country = "NL"

// Task 1.3c
//    Problem: The lat/lon is for London.
//    Approach: Lookup the lat/lon for Schiphol airport on wikipedia and adjust here
let london =
    { Latitude = 51.5074
      Longitude = 0.1278 }

let getLocationResponse postcode = async {
    if not (Validation.isValidPostcode country postcode) then
        failwith "Invalid postcode"

    let! location = getLocation postcode

    // Task 1.3b
    //   Problem: We want Schiphol, not London.
    //   Approach: Right click on 'london', select Rename Symbol and rename to 'schiphol'
    let distanceToAirport = getDistanceBetweenPositions location.LatLong london

    let response =
        { Postcode = postcode
          Location = location
          DistanceToAirport = (distanceToAirport / 1000.<meter>) }
    return response
}

let private asWeatherResponse (weather: Weather.OpenMeteoSearch.Root) =
    let averageTemp =
        weather.Hourly.Temperature2m
        |> Array.average
        |> float

    let (|LessOrEqual|_|) threshold v =
        if v <= threshold then Some ()
        else None

    let weatherType =
        // WMO Weather interpretation codes (WW)
        // https://open-meteo.com/en/docs
        (*
0	Clear sky
1, 2, 3	Mainly clear, partly cloudy, and overcast
45, 48	Fog and depositing rime fog
51, 53, 55	Drizzle: Light, moderate, and dense intensity
56, 57	Freezing Drizzle: Light and dense intensity
61, 63, 65	Rain: Slight, moderate and heavy intensity
66, 67	Freezing Rain: Light and heavy intensity
71, 73, 75	Snow fall: Slight, moderate, and heavy intensity
77	Snow grains
80, 81, 82	Rain showers: Slight, moderate, and violent
85, 86	Snow showers slight and heavy
95 *	Thunderstorm: Slight or moderate
96, 99 *	Thunderstorm with slight and heavy hail
        *)
        match weather.CurrentWeather.Weathercode with
        | LessOrEqual 0  -> WeatherType.Clear
        | LessOrEqual 3  -> WeatherType.Hail
        // have fun filling the blanks if you know weather english lexical
        // and above table makes sense
        | LessOrEqual 48 -> WeatherType.Showers
        | LessOrEqual 67 -> WeatherType.Sleet
        | LessOrEqual 86 -> WeatherType.Snow
        | LessOrEqual 95 -> WeatherType.Thunder
        | _ -> WeatherType.Apocalyptic

    { WeatherType = weatherType
      AverageTemperature = averageTemp }

let getWeather postcode = async {
    let! loc = GeoLocation.getLocation postcode
    let! weather = Weather.getWeatherForPosition loc.LatLong
    let response = weather |> asWeatherResponse
    return response
}

let dojoApi =
    { GetLocation = getLocationResponse
      GetWeather = getWeather
    }