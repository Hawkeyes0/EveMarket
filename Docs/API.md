# APIs

## /markets/groups
返回市场商品分组信息
```
Group {
    number id, 
    number parentId, 
    string name,
    string description,
    number iconId,
    number hasTypes,
    Group[] childrens
}
```

## /markets/groups/{groupId}/types
返回分类下所有商品信息
```
Type {
    number id,
    string name,
    string description,
    number marketGroupId
}
```

## /markets/types/{typeId}
返回商品信息
```
Result {
    Type type,
    TradeStats sellStats,
    TradeStats buyStats,
    Order[] sell,
    Order[] buy
}

Type {
    number id,
    number groupId,
    string name,
    string description,
    number volume,
    number marketGroupId
}

TradeStats {
    float average,
    float weighted_average,
    float median,
    number max,
    number min,
    float variance,
    float stdDev,
    float fivePercent,
    number volume,
    number orderCount
}

Order {
    number id,
    number regionId,
    number systemId,
    number stationId,
    number typeId,
    number buy,
    number price,
    number volumeEntered,
    number volumeRemain,
    number volumeMin,
    string range,
    number duration,
    Date issuedAt,
    Date receivedAt,
    Region region,
    Station station
}
```

## /regions
获取星区列表
```
Region {
    number id,
    string name
}

Station {
    number id,
    number regionId,
    number constellationId,
    number systemId,
    number corporationId,
    string type,
    float security,
    string name
}
```

## /types/{id}
获取商品细节
```
Type {
    number id,
    number groupId,
    string name,
    string description,
    number volume,
    number volumePackaged,
    number capacity,
    number mass,
    AttributeCategory[] attributeCategories,
    number marketGroupId
    Type blueprint,
    Type[] reprocessedMaterials,
    Meta[] metas,
}

AttributeCategory {
    number id,
    string name,
    number position,
    Attribute[] attributes
}

Attribute {
    number id,
    string name,
    string displayName,
    float value,
    boolean published,
    number iconId,
    number unitId,
    string unitName,
    number position
}

Meta {
    number id,
    string name,
    Type[] types
}
```