
namespace global

module List =
    // Return a new list with the item it the given index removed
    let removeAt idx xs =
        xs
        |> List.indexed
        |> List.filter (fun (i,v) -> i <> idx)
        |> List.map snd

    // Return a new list with the item it the given index replaced
    let setAt idx v xs =
        let n = List.length xs
        if idx > n then
            failwith "invalid set"
        elif idx = n then
            List.append xs [v]
        elif idx = -1 then
            List.append [v] xs
        else
            xs
            |> List.indexed
            |> List.map (fun (i,v2) -> if i = idx then v else v2)


