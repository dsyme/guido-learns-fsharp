module DataAccess

open FSharp.Data
open Shared

[<AutoOpen>]
module GeoLocation =
    open FSharp.Data.UnitSystems.SI.UnitNames
    let country = "NL"
    type PostcodesIO = JsonProvider<"http://api.geonames.org/postalCodeLookupJSON?postalcode=1011&country=NL&username=dsyme">

    let getLocation postcode = async {
        let! info =
            $"http://api.geonames.org/postalCodeLookupJSON?postalcode={postcode}&country=NL&username=dsyme"
            |> PostcodesIO.AsyncLoad
        if info.Postalcodes.Length > 0 then
            let code = info.Postalcodes.[0]
            return
                { LatLong = { Latitude = float code.Lat; Longitude = float code.Lng }
                  Town = code.AdminName2
                  Region = code.AdminName1 }
        else
           return! failwith $"no postcodes returned for {postcode} for country {country}" }

    let getDistanceBetweenPositions pos1 pos2 =
        let lat1, lng1 = pos1.Latitude, pos1.Longitude
        let lat2, lng2 = pos2.Latitude, pos2.Longitude
        let inline degreesToRadians degrees = System.Math.PI * float degrees / 180.0
        let r = 6371000.0
        let phi1 = degreesToRadians lat1
        let phi2 = degreesToRadians lat2
        let deltaPhi = degreesToRadians (lat2 - lat1)
        let deltaLambda = degreesToRadians (lng2 - lng1)
        let a = sin (deltaPhi / 2.0) * sin (deltaPhi / 2.0) + cos phi1 * cos phi2 * sin (deltaLambda / 2.0) * sin (deltaLambda / 2.0)
        let c = 2.0 * atan2 (sqrt a) (sqrt (1.0 - a))
        r * c * 1.<meter>
#if METAWEATHER_ALIVE

[<AutoOpen>]
module Weather =
    type MetaWeatherSearch = JsonProvider<"https://www.metaweather.com/api/location/search/?lattlong=51.5074,0.1278">
    type MetaWeatherLocation = JsonProvider<"https://www.metaweather.com/api/location/1393672">
    let getWeatherForPosition location = async {
        let! locations =
            (location.Latitude, location.Longitude)
            ||> sprintf "https://www.metaweather.com/api/location/search/?lattlong=%f,%f"
            |> MetaWeatherSearch.AsyncLoad
        let bestLocationId = locations |> Array.sortBy (fun t -> t.Distance) |> Array.map (fun o -> o.Woeid) |> Array.head
        return!
            bestLocationId
            |> sprintf "https://www.metaweather.com/api/location/%d"
            |> MetaWeatherLocation.AsyncLoad }*)

#else

module Weather =

    type OpenMeteoSearch = JsonProvider<"https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m">

    let getWeatherForPosition location = async {
        let uri = $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m"
        let! result = OpenMeteoSearch.AsyncLoad uri

        return result
    }
#endif