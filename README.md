# Интернет Магазин Shopee - Web API приложение

## Описание проекта
Данный проект представляет собой интернет-магазин Shopee, реализованный в виде Web API приложения. На текущем этапе работы осуществляется формирование архитектуры системы.

https://github.com/user-attachments/assets/4278dddc-4afa-4a41-87c1-42fb2675ebe2

## Реализованно
- Получение товаров WebUI <- API <- База данных.
- Поиск товаров.
- Фильтрация товаров по цене, рейтингу, названию и по возрастанию, убыванию.
- Создание товара и сохранение в БД.

## Текущая задача
На данный момент реализуется ограниченная загрузка товаров на странице из API.
Деление товаров на категории.

## Планы по развитию
В рамках дальнейшей разработки планируется:

- Реализация микросервисной архитектуры
- Поднятие сервисов в Docker
- Реализация архитектурного паттерна для ASP.NET Core сервисов - Чистой Архитектуры
- Использование современных технологий для микросервисной архитектуры:

  - **Web UI**: JavaScript, React
  - **API Gateway**: NGINX
  - **Сервисы**: ASP.NET Core с Чистой Архитектурой и связкой по HTTP с RESTful API
  - **База данных**: PostgreSQL
  - **Брокер сообщений**: Kafka
  - **Мониторинг**: Kubernetes, Grafana
  - **Отладка**: Логирование, Unit-тесты с NUnit
