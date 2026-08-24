local task = {}

function task.wait(seconds)
    coroutine.yield({
        command = "wait",
        duration = seconds
    })
end

return task