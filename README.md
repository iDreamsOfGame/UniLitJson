# UniLitJson

[![License](https://img.shields.io/github/license/iDreamsOfGame/UniLitJson)](https://github.com/iDreamsOfGame/UniLitJson/blob/master/LICENSE)
[![openupm](https://img.shields.io/npm/v/io.github.idreamsofgame.unilitjson?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/io.github.idreamsofgame.unilitjson/)

A Unity package to handle conversions from and to JSON (JavaScript Object Notation) strings. UniLitJson packages the complete C# source of [LitJson](https://github.com/LitJson/LitJson) 0.19.0 and adds a set of Unity-friendly extensions on top of it.

## Features

The full LitJson 0.19.0 source code, in the original `LitJson` namespace — a drop-in replacement for existing LitJson code, with no precompiled DLLs:

| API | Description |
| --- | --- |
| `JsonMapper` | Maps between .NET objects and JSON text. |
| `JsonData` | A dynamic JSON tree that can be built and inspected at runtime. |
| `JsonReader` | Streaming, forward-only JSON reader (token-based). |
| `JsonWriter` | Streaming JSON writer (token-based). |

Extensions added by this package:

- `[JsonIgnore]` attribute to exclude fields and properties from serialization.
- Exporters/importers for Unity built-in types: `Vector2`, `Vector3`, `Vector4`, `Quaternion`, `Color`, `Color32`, `Bounds`, `Rect`, `RectOffset` and `System.Type` (serialized as its full name).
- `WriteProperty` extension methods on `JsonWriter` for `long`, `string`, `bool` and `double` values.
- Non-generic `JsonMapper.ToObject(string json, Type convertType)` overload.

An editor test suite (NUnit) is included under `Tests/`.

## Requirements

- Unity **2021.3** or later.

## Installation

The package name is `io.github.idreamsofgame.unilitjson`. Choose one of the two methods below.

### Method 1: Via Git URL

1. Open Unity and go to **Window &gt; Package Manager**.
2. Click the **+** button in the top-left corner and choose **Add package from git URL...**
3. Enter the following URL and click **Add**:

   ```
   https://github.com/iDreamsOfGame/UniLitJson.git?path=Assets/UniLitJson#0.19.0
   ```

   > Remove the `#0.19.0` suffix to track the default branch instead of a pinned release.

Alternatively, add it directly to your project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "io.github.idreamsofgame.unilitjson": "https://github.com/iDreamsOfGame/UniLitJson.git?path=Assets/UniLitJson#0.19.0"
  }
}
```

### Method 2: Via OpenUPM

The package is published on [OpenUPM](https://openupm.com/packages/io.github.idreamsofgame.unilitjson/).

If you have the [OpenUPM CLI](https://github.com/openupm/openupm-cli) installed, simply run the following command in your project folder:

```bash
openupm add io.github.idreamsofgame.unilitjson
```

Otherwise, add a scoped registry in your project's `Packages/manifest.json`:

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": ["io.github.idreamsofgame"]
    }
  ],
  "dependencies": {
    "io.github.idreamsofgame.unilitjson": "0.19.0"
  }
}
```

You can also configure this in the Unity Editor: open **Edit &gt; Project Settings &gt; Package Manager**, add a new scoped registry with the URL `https://package.openupm.com` and the scope(s) `io.github.idreamsofgame`, then install **UniLitJson** from **Window &gt; Package Manager &gt; My Registries**.

## Usage

All APIs live in the `LitJson` namespace:

```csharp
using LitJson;
```

### Serialize an object to JSON

```csharp
public class Player
{
    public string name;
    public int level;
    [JsonIgnore] public string password;   // Excluded from serialization
}

var player = new Player { name = "Alice", level = 10, password = "secret" };
string json = JsonMapper.ToJson(player);
// {"name":"Alice","level":10}
```

### Deserialize JSON to an object

```csharp
Player player = JsonMapper.ToObject<Player>(json);

// Non-generic overload, useful when the type is only known at runtime
Type type = typeof(Player);
object obj = JsonMapper.ToObject(json, type);
```

### Work with dynamic JSON (`JsonData`)

`JsonData` lets you parse and manipulate JSON without defining a class:

```csharp
JsonData data = JsonMapper.ToObject(@"{""name"":""Alice"",""skills"":[""fire"",""ice""]}");

string name = (string)data["name"];          // "Alice"
string skill = (string)data["skills"][0];    // "fire"

data["level"] = 10;                          // Add a new value
string output = data.ToJson();
```

### Streaming with `JsonReader` and `JsonWriter`

For low-level, token-based control:

```csharp
var writer = new JsonWriter();
writer.WriteObjectStart();
writer.WriteProperty("name", "Alice");   // Extension method: name + string value
writer.WriteProperty("level", 10);       // Extension method: name + long value
writer.WriteObjectEnd();

string json = writer.ToString();         // {"name":"Alice","level":10}
```

```csharp
var reader = new JsonReader(json);
JsonData data = JsonMapper.ToObject(reader);
```

### Serialize Unity built-in types

Unity types are registered by `UnityTypeBindings` and serialize as plain objects:

```csharp
var position = new Vector3(1f, 2f, 3f);
string json = JsonMapper.ToJson(position);
// {"x":1.0,"y":2.0,"z":3.0}

Vector3 back = JsonMapper.ToObject<Vector3>(json);
```

Registration behavior:

- **In the Editor**, the bindings are registered automatically on load via `[InitializeOnLoad]`.
- **In player builds**, nothing references the `UnityTypeBindings` class, so its static constructor never runs on its own (and code stripping may remove it). Register it once at startup:

```csharp
using UnityEngine;
using LitJson;

public static class JsonBindingsInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        UnityTypeBindings.Register();
    }
}
```

## License

This package is released under the [MIT License](LICENSE).

LitJson itself is also licensed under the MIT License. See [Assets/UniLitJson/LICENSE.md](Assets/UniLitJson/LICENSE.md) for details.
