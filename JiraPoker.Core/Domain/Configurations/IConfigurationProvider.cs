﻿namespace JiraPoker.Core.Domain.Configurations;

public interface IConfigurationProvider
{
    T? GetValue<T>(string key);
    string GetValueOrDefault(string key);
}