[System.Serializable]
public class Animation
{
    public AnimationDefinition definition;
    public string id;
    
    public Animation(AnimationDefinition def)
    {
        definition = def;
        id = definition.id;
    }
}

[System.Serializable]
public class AnimationDefinition
{
    public string id;
    public bool loop;
    public AnimationFrame[] frames;

    public AnimationDefinition(
        string animId,
        bool animLoop,
        AnimationFrame[] animFrames)
    {
        id = animId;
        loop = animLoop;
        frames = animFrames;
    }
}

[System.Serializable]
public class AnimDefinitionData
{
    public string id;
    public bool loop;
    public AnimationFrame[] frames;
}

[System.Serializable]
public class AnimGroupData
{
    public string groupId;
    public AnimDefinitionData[] animations;
}

[System.Serializable]
public class AnimDefinitionFile
{
    public AnimGroupData[] animations;
}

[System.Serializable]
public class AnimationFrame
{
    public string id;
    public string path;
    public short ticks;
}
